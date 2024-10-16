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
        List<Auction> auctions = _auctionPersistence.GetAllByUserName(userName);
        return auctions;
    }

    public Auction GetById(int id, string userName)
    {
        Auction auction = _auctionPersistence.GetById(id, userName);
        if (auction == null)
        {
            throw new DataException("Auction not found");
        }
        
        return auction;
    }

    public bool Add(string title, string description, DateTime endDate, int price, string userName)
    {
        if (title == null || title.Length > 100) throw new DataException("Title cannot be null or more than 100 characters");
        if (description == null) throw new DataException("Description cannot be null");
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
}