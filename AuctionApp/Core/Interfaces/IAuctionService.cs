namespace AuctionApp.Core.Interfaces;

public interface IAuctionService
{
    List<Auction> ListYourAuctions(string userName);
    
    Auction GetAuctionById(int id);
    
    bool IsOwner(int id, string userName);
    
    void CreateAuction(string title, string description, DateTime endDate, int price, string userName);
    
    void EditDescription(int id, string description, string userName);
    
    Auction GetDetails(Auction auction);

    List<Auction> ListAllActiveAuctions();
    
    List<Auction> ListAllWonAuctions(string userName);
    
    void Bid(int price, int auctionId, string userName);
    
    List<Auction> ListBiddedAuctions(string userName);
}