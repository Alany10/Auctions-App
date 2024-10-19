using Microsoft.EntityFrameworkCore;

namespace AuctionApp.Persistence
{
    public class AuctionDbContext : DbContext
    {
        public AuctionDbContext(DbContextOptions<AuctionDbContext> options) : base(options) {}
        
        public DbSet<BidDb> BidDbs { get; set; }
        public DbSet<AuctionDb> AuctionDbs { get; set; }
    }
}