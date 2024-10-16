namespace AuctionApp.Core.Interfaces;

public interface IAuctionService
{
    List<Auction> GetAllByUserName(string userName);
    
    Auction GetById(int id, string userName);
    
    bool Add(string title, string description, DateTime endDate, int price, string userName);
}