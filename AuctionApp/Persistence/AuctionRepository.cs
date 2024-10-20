using AuctionApp.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AuctionApp.Persistence;

public class AuctionRepository : GenericRepository<AuctionDb>, IAuctionRepository
{
    private readonly AuctionDbContext _dbContext;

    public AuctionRepository(AuctionDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public void EditDescription(AuctionDb auctionDb)
    {
        // Hämta den existerande auktionen från databasen
        AuctionDb existingAuction = _dbContext.AuctionDbs.Find(auctionDb.Id);

        // Uppdatera värdena på den existerande auktionen
        existingAuction.Description = auctionDb.Description; //TODO

        // Spara ändringarna
        _dbContext.SaveChanges();
    }

    public List<AuctionDb> GetAllActiveAuctions()
    {
        return _dbContext.AuctionDbs
            .Where(a => a.EndDate > DateTime.Now)
            .ToList();
    }

    public AuctionDb GetDetails(AuctionDb auctionDb)
    {
        return _dbContext.AuctionDbs
            .Include(a => a.BidDbs)
            .FirstOrDefault(a => a.Id == auctionDb.Id);
    }


    public List<AuctionDb> GetAllBiddedAuctions(string userName)
    {
        return _dbContext.AuctionDbs
            .Include(a => a.BidDbs) // Laddar också alla relaterade bud
            .Where(a => a.BidDbs.Any(b => b.UserName == userName) && a.EndDate > DateTime.Now) // Filtrerar auktioner med bud från användaren och som är pågående
            .Select(a => a) // Välj hela auktionen
            .Distinct() // Tar bort dubbletter
            .ToList();
    }



    public List<AuctionDb> GetAllWonAuctions(string userName) 
    {
        return _dbContext.AuctionDbs
            .Include(a => a.BidDbs) // Inkludera relaterade bud
            .Where(a => a.BidDbs.Any(b => b.UserName.Equals(userName) && b.Price == a.Price) && a.EndDate < DateTime.Now) // Filtrera för att få auktioner där användaren har det högsta budet
            .ToList(); // Konvertera till en lista
    }
    
}