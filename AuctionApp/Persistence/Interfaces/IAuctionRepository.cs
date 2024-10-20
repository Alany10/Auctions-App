namespace AuctionApp.Core.Interfaces;

using AuctionApp.Persistence;

public interface IAuctionRepository : IGenericRepository<AuctionDb>
{
    void EditDescription(AuctionDb auctionDb);
    
    List<AuctionDb> GetAllActiveAuctions();
    
    AuctionDb GetDetails(AuctionDb auctionDb);
    
    List<AuctionDb> GetAllBiddedAuctions(string userName);
    
    List<AuctionDb> GetAllWonAuctions(string userName);
}