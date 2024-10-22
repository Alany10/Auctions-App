namespace AuctionApp.Persistence.Interfaces;

public interface IAuctionRepository : IGenericRepository<AuctionDb>
{
    void EditDescription(AuctionDb auctionDb);
    
    List<AuctionDb> GetAllActiveAuctions();
    
    AuctionDb GetDetails(AuctionDb auctionDb);
    
    List<AuctionDb> GetAllBiddedAuctions(string userName);
    
    List<AuctionDb> GetAllWonAuctions(string userName);
}