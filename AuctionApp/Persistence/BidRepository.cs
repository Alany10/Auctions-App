using AuctionApp.Core.Interfaces;

namespace AuctionApp.Persistence;

public class BidRepository : GenericRepository<BidDb>, IBidRepository
{
    private readonly AuctionDbContext _dbContext;

    public BidRepository(AuctionDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}