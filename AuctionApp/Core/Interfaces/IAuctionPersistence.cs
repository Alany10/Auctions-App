namespace AuctionApp.Core.Interfaces;

public interface IAuctionPersistence
{
    List<Auction> GetAllByUserName(string userName);
    
    Auction GetById(int id);
    
    void Create(Auction auction);
    
    void Create(Bid bid);
    
    void EditDescription(Auction auction);
    
    List<Auction> GetAllActiveAuctions();
    
    Auction GetDetails(Auction auction);
    
    List<Auction> GetAllBiddedAuctions(string userName);
    
    List<Auction> GetAllWonAuctions(string userName);
}