namespace AuctionApp.Core.Interfaces;

public interface IAuctionPersistence
{
    List<Auction> GetAll();
    
    List<Auction> GetAllByUserName(string userName);
    
    Auction GetById(int id, string userName);
    
    bool Save(Auction auction);
}