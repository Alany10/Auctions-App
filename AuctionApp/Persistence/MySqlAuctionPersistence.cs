using System.Data;
using AuctionApp.Core;
using AuctionApp.Core.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AuctionApp.Persistence;

public class MySqlAuctionPersistence : IAuctionPersistence
{
    private readonly AuctionDbContext _dbContext;
    private readonly IMapper _mapper;

    public MySqlAuctionPersistence(AuctionDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public List<Auction> GetAll()
    {
        var auctionDbs = _dbContext.AuctionDbs
            .Include(a => a.BidDbs)
            .ToList();

        List<Auction> result = new List<Auction>();
        foreach (AuctionDb adb in auctionDbs)
        {
            Auction auction = _mapper.Map<Auction>(adb);
            foreach (BidDb bidDb in adb.BidDbs)
            {
                auction.AddBid(_mapper.Map<Bid>(bidDb));
            }
            result.Add(auction);
        }
        
        return result;
    }

    public List<Auction> GetAllByUserName(String userName)
    {
        var auctionDbs = _dbContext.AuctionDbs
            .Where(a => a.UserName == userName)
            .ToList();

        List<Auction> result = new List<Auction>();
        foreach (AuctionDb adb in auctionDbs)
        {
            Auction auction = _mapper.Map<Auction>(adb);
            result.Add(auction);
        }
        
        return result;
    }

    public Auction GetById(int id)
    {
        AuctionDb auctionDb = _dbContext.AuctionDbs
            .Where(a => a.Id == id)
            .Include(a => a.BidDbs)
            .FirstOrDefault(); // null if not found

        if (auctionDb == null)
        {
            throw new DataException("No auction found");
        }
        
        Auction auction = _mapper.Map<Auction>(auctionDb);
        foreach (BidDb bidDb in auctionDb.BidDbs)
        {
            Bid bid = _mapper.Map<Bid>(bidDb);
            auction.AddBid(bid);
        }
        
        return auction;
    }

    public bool Save(Auction auction)
    {
        AuctionDb adb = _mapper.Map<AuctionDb>(auction);
        _dbContext.AuctionDbs.Add(adb);
        _dbContext.SaveChanges();

        return true;
    }

    public bool Update(Auction auction)
    {
        // Hämta den existerande auktionen från databasen
        AuctionDb existingAuction = _dbContext.AuctionDbs.Find(auction.Id);

        // Uppdatera värdena på den existerande auktionen
        existingAuction.Description = auction.Description;

        // Spara ändringarna
        _dbContext.SaveChanges();
    
        return true;
    }

    public bool Bid(Bid bid, int auctionId) 
    {
        // Hämta auktionen för att kontrollera nuvarande pris
        var auction = _dbContext.AuctionDbs.Find(auctionId);
        if (auction == null)
        {
            // Hantera fel om auktionen inte hittas (kan kasta ett undantag eller returnera false)
            throw new InvalidOperationException($"Auction with ID {auctionId} does not exist.");
        }
    
        // Konvertera Bid till BidDb
        BidDb bdb = _mapper.Map<BidDb>(bid);
        bdb.AuctionId = auctionId;

        // Om det nya budet är högre än det nuvarande priset, uppdatera auktionspriset
        if (bdb.Price > auction.Price)
        {
            auction.Price = bdb.Price; // Uppdatera priset
        }
    
        // Lägg till budet i databasen
        _dbContext.BidDbs.Add(bdb);
    
        // Spara ändringar i databasen
        _dbContext.SaveChanges();
    
        return true;
    }

}