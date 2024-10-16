using AuctionApp.Core.Interfaces;

namespace AuctionApp.Core;

public class MockAuctionService : IAuctionService
{
    public List<Auction> GetAllByUserName(string userName)
    {
        return _auctions;
    }

    public Auction GetById(int id, string userName)
    {
        return _auctions.Find(a => a.Id == id && a.UserName == userName);
    }

    public bool Add(string title, string description, DateTime endDate, int price, string userName)
    {
        throw new NotImplementedException("MockAuctionService doesn't support add");
    }
    
    private static readonly List<Auction> _auctions = new();

    static MockAuctionService()
    {
        // Skapar några hårdkodade auktioner
        Auction a1 = new Auction(1, "Car", "A nice red sports car", DateTime.Now.AddDays(10), 10000, "user1");
        Auction a2 = new Auction(2, "Laptop", "High-end gaming laptop", DateTime.Now.AddDays(5), 1500, "user2");
        a2.AddBid(new Bid(1, 10000, "user1"));
        a2.AddBid(new Bid(2, 15000, "user2"));
        a1.AddBid(new Bid(3, 15500, "user3"));
        _auctions.Add(a1);
        _auctions.Add(a2);
    }
}