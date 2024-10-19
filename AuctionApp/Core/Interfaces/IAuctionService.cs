namespace AuctionApp.Core.Interfaces;

public interface IAuctionService
{
    List<Auction> GetAllByUserName(string userName);
    
    Auction GetById(int id);
    
    bool IsOwner(int id, string userName);
    
    bool Add(string title, string description, DateTime endDate, int price, string userName);
    
    bool editDescription(int id, string description, string userName);

    public List<Auction> ListAllAuctions();
    
    public bool Bid(int price, int auctionId, string userName);
    
    public List<Auction> ListAllyourAuctions(string userName);
}