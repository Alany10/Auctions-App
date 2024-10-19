namespace AuctionApp.Core.Interfaces;

public interface IAuctionPersistence
{
    List<Auction> GetAll();
    
    List<Auction> GetAllByUserName(string userName);
    
    Auction GetById(int id);
    
    bool Save(Auction auction);
    
    bool Update(Auction auction);

    public bool Bid(Bid bid, int auctionId);
}