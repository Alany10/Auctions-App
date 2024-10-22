using System.Data;
using AuctionApp.Core.Interfaces;

namespace AuctionApp.Core;

public class AuctionService : IAuctionService
{
    private readonly IAuctionPersistence _auctionPersistence;

    public AuctionService(IAuctionPersistence auctionPersistence)
    {
        _auctionPersistence = auctionPersistence;
    }

    public List<Auction> ListYourAuctions(string userName)
    {
        if (userName == null) throw new ArgumentNullException(); 
        List<Auction> auctions = _auctionPersistence.GetAllByUserName(userName);

        return auctions.OrderBy(a => a.EndDate).ToList();
    }

    public Auction GetAuctionById(int id)
    {
        Auction auction = _auctionPersistence.GetById(id);
        if (auction == null)
        {
            throw new DataException("Auction not found");
        }

        return auction;
    }

    public bool IsOwner(int id, string userName)
    {
        Auction auction = _auctionPersistence.GetById(id);
        if (auction == null)
        {
            throw new DataException("Auction not found");
        }

        return auction.UserName.Equals(userName);
    }

    public void CreateAuction(string title, string description, DateTime endDate, int price, string userName)
    {
        if (title == null || title.Length > 100)
            throw new DataException("Title cannot be null or more than 100 characters");

        if (description == null) throw new DataException("Description cannot be null");

        // Kontrollera om endDate är i framtiden
        if (endDate <= DateTime.Now) throw new DataException("End date must be a future date.");

        if (price < 1) throw new DataException("Price must be valid");

        if (userName == null) throw new DataException("User name cannot be null");

        Auction newAuction = new Auction(title, description, endDate, price, userName);

        _auctionPersistence.Create(newAuction);
    }

    public void EditDescription(int id, string description, string userName)
    {
        Auction auction = _auctionPersistence.GetById(id);
        if (auction == null) throw new DataException("Auction not found");

        if (description == null) throw new DataException("Description cannot be null");
        if (description.Equals(auction.Description)) throw new DataException("No changes made");

        if (!IsOwner(id, userName)) throw new UnauthorizedAccessException("User does not belong to this auction");

        auction.Description = description;
        _auctionPersistence.EditDescription(auction);
    }

    public Auction GetDetails(Auction auction)
    {
        if (auction == null) throw new DataException("Auction not found");
        return _auctionPersistence.GetDetails(auction);
    }

    public List<Auction> ListAllActiveAuctions()
    {
        List<Auction> auctions = _auctionPersistence.GetAllActiveAuctions();

        return auctions.OrderBy(a => a.EndDate).ToList();
    }

    public void Bid(int price, int auctionId, string userName)
    {
        Auction auction = _auctionPersistence.GetById(auctionId);

        if (auction == null) throw new DataException("Auction not found");
        
        if (price <= auction.Price) throw new DataException("Price must be higher");
        
        if (userName == null) throw new DataException("User name cannot be null");

        if (auction.UserName.Equals(userName)) throw new DataException("User owns this auction");
        
        Bid newBid = new Bid(price, userName, auctionId);
        if (newBid.BidDate >= auction.EndDate) throw new DataException("Auction expired");
        
        auction.AddBid(newBid);
        
        _auctionPersistence.Create(newBid);
    }

    public List<Auction> ListBiddedAuctions(string userName)
    {
        if (userName == null) throw new ArgumentNullException(); 
        List<Auction> auctions = _auctionPersistence.GetAllBiddedAuctions(userName).ToList();

        return auctions.OrderBy(a => a.EndDate).ToList();
    }

    public List<Auction> ListAllWonAuctions(string userName)
    {
        if (userName == null) throw new ArgumentNullException(); 
        List<Auction> auctions = _auctionPersistence.GetAllWonAuctions(userName).ToList();

        return auctions.Where(a => a.EndDate < DateTime.Now).ToList();
    }
}