using AuctionApp.Persistence.Interfaces;

namespace AuctionApp.Persistence
{
    public class BidRepository : GenericRepository<BidDb>, IBidRepository
    {
        public BidRepository(AuctionDbContext dbContext) : base(dbContext)
        {
        }
    }
}