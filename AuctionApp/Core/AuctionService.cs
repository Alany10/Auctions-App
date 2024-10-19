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

    public List<Auction> GetAllByUserName(string userName)
    {
        // Hämtar alla auktioner från din datalagring
        List<Auction> auctions = _auctionPersistence.GetAllByUserName(userName);

        // Filtrerar bort alla auktioner som är avslutade och sorterar de återstående efter EndDate
        List<Auction> sortedAuctions = auctions
            .OrderBy(auction => auction.EndDate) // Sortera efter EndDate
            .ToList();

        return sortedAuctions;
    }

    public Auction GetById(int id)
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

    public bool Add(string title, string description, DateTime endDate, int price, string userName)
    {
        if (title == null || title.Length > 100)
            throw new DataException("Title cannot be null or more than 100 characters");

        if (description == null) throw new DataException("Description cannot be null");

        // Kontrollera om endDate är i framtiden
        if (endDate <= DateTime.Now) throw new DataException("End date must be a future date.");

        if (price < 1) throw new DataException("Price must be valid");

        if (userName == null) throw new DataException("User name cannot be null");

        Auction newAuction = new Auction(title, description, endDate, price, userName);

        List<Auction> allAuctions = _auctionPersistence.GetAll();

        foreach (Auction auction in allAuctions)
        {
            if (Auction.IsEqual(newAuction, auction))
            {
                return false;
            }
        }

        _auctionPersistence.Save(newAuction);
        return true;
    }

    public bool editDescription(int id, string description, string userName)
    {
        Auction auction = _auctionPersistence.GetById(id);
        if (auction == null) throw new DataException("Auction not found");

        if (description == null) throw new DataException("Description cannot be null");
        if (description.Equals(auction.Description)) throw new DataException("No changes made");

        if (!IsOwner(id, userName)) throw new UnauthorizedAccessException("User does not belong to this auction");

        auction.Description = description;
        _auctionPersistence.Update(auction);
        return true;
    }

    public List<Auction> ListAllActiveAuctions()
    {
        // Hämtar alla auktioner från din datalagring
        List<Auction> auctions = _auctionPersistence.GetAll();

        // Filtrerar bort alla auktioner som är avslutade och sorterar de återstående efter EndDate
        List<Auction> activeAuctions = auctions
            .Where(auction => auction.IsCompleted()) // Filtrera bort avslutade auktioner
            .OrderBy(auction => auction.EndDate) // Sortera efter EndDate
            .ToList();

        return activeAuctions;
    }

    public bool Bid(int price, int auctionId, string userName)
    {
        Auction auction = _auctionPersistence.GetById(auctionId);

        if (auction == null) throw new DataException("Auction not found");
        
        if (price <= auction.Price) throw new DataException("Price must be higher");
        
        if (userName == null) throw new DataException("User name cannot be null");

        Bid newBid = new Bid(price, userName);
        if (newBid.BidDate < auction.EndDate) auction.AddBid(newBid);
        else throw new DataException("Auction expired");

        _auctionPersistence.Bid(newBid, auctionId);
        return true;
    }

    public List<Auction> ListAllyourAuctions(string userName)
    {
        List<Auction> auctions = _auctionPersistence.GetAll().Where(auction => auction.IsCompleted()).ToList();
        List<Auction> yourAuctions = new List<Auction>();

        foreach (Auction auction in auctions)
        {
            foreach (Bid bid in auction.Bids)
            {
                if (bid.UserName.Equals(userName))
                {
                    yourAuctions.Add(auction);
                    break;
                }
            }
        }
        
        yourAuctions
            .OrderBy(auction => auction.EndDate) // Sortera efter EndDate
            .ToList();
        
        return yourAuctions;
    }

    public List<Auction> ListAllWonAuctions(string userName)
    {
        // Hämta alla avslutade auktioner
        List<Auction> auctions = _auctionPersistence.GetAll()
            .Where(a => !a.IsCompleted())
            .ToList();

        List<Auction> wonAuctions = new List<Auction>();

        // Loop genom varje auktion
        foreach (Auction auction in auctions)
        {
            // Kontrollera om auktionen har bud
            if (auction.Bids.Any())
            {
                // Hitta det högsta budet för auktionen
                Bid highestBid = auction.Bids.OrderByDescending(b => b.Price).First();

                // Kontrollera om användarnamnet för det högsta budet matchar det angivna användarnamnet
                if (highestBid.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase))
                {
                    wonAuctions.Add(auction);
                }
            }
        }

        return wonAuctions;
    }

    
}