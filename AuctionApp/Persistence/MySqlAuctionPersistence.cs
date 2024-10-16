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
            .ToList();

        List<Auction> result = new List<Auction>();
        foreach (AuctionDb adb in auctionDbs)
        {
            Auction auction = _mapper.Map<Auction>(adb);
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

    public Auction GetById(int id, string userName)
    {
        AuctionDb auctionDb = _dbContext.AuctionDbs
            .Where(a => a.Id == id && a.UserName.Equals(userName))
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
    
}