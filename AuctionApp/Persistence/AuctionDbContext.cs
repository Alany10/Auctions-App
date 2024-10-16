using Microsoft.EntityFrameworkCore;

namespace AuctionApp.Persistence
{
    public class AuctionDbContext : DbContext
    {
        public AuctionDbContext(DbContextOptions<AuctionDbContext> options) : base(options) {}
        
        public DbSet<BidDb> BidDbs { get; set; }
        public DbSet<AuctionDb> AuctionDbs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            AuctionDb adb = new AuctionDb
            {
                Id = -1,
                Title = "Laptop",
                Description = "I7, 16GB ram, 512GB storage",
                EndDate = DateTime.Now.AddDays(7),
                Price = 700,
                UserName = "admin",
                BidDbs = new List<BidDb>()
            };
            modelBuilder.Entity<AuctionDb>().HasData(adb);

            BidDb bdb1 = new BidDb
            {
                Id = -1,
                Price = 800,
                AuctionId = -1,
                UserName = "Alan"
            };
            
            BidDb bdb2 = new BidDb
            {
                Id = -2,
                Price = 1000,
                AuctionId = -1,
                UserName = "Ali"
            };

            modelBuilder.Entity<BidDb>().HasData(bdb1, bdb2);

        }
    }
}