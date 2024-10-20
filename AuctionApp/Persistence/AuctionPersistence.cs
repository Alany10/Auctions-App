using AuctionApp.Core;
using AuctionApp.Core.Interfaces;
using AutoMapper;

namespace AuctionApp.Persistence;

public class AuctionPersistence : IAuctionPersistence
{
    private readonly AuctionRepository _auctionRepository;
    private readonly BidRepository _bidRepository;
    private readonly IMapper _mapper;

    public AuctionPersistence(AuctionDbContext dbContext, IMapper mapper) 
    {
        _auctionRepository = new AuctionRepository(dbContext);
        _bidRepository = new BidRepository(dbContext);
        _mapper = mapper;
    }

    public List<Auction> GetAllByUserName(string userName)
    {
        List<AuctionDb> auctionDbs = _auctionRepository.GetAllByUserName(userName);
        List<Auction> auctions = new List<Auction>();
        
        foreach (AuctionDb auctionDb in auctionDbs)
        {
            auctions.Add(_mapper.Map<Auction>(auctionDb));
        }
        
        return auctions;
    }


    public Auction GetById(int id)
    { 
        AuctionDb auctionDb = _auctionRepository.GetById(id);
        Auction auction = _mapper.Map<Auction>(auctionDb);
        
        return auction;
    }

    public void Create(Auction auction)
    {
        AuctionDb auctionDb = _mapper.Map<AuctionDb>(auction);
        
        _auctionRepository.Create(auctionDb);
    }
    
    public void Create(Bid bid)
    {
        BidDb bidDb = _mapper.Map<BidDb>(bid);
        
        _bidRepository.Create(bidDb);
        
        AuctionDb auctionDb = _mapper.Map<AuctionDb>(_auctionRepository.GetById(bidDb.AuctionId));
        auctionDb.Price = bidDb.Price;
        _auctionRepository.Update(auctionDb);
    }

    public void EditDescription(Auction auction)
    {
        AuctionDb auctionDb = _mapper.Map<AuctionDb>(auction);
        
        _auctionRepository.EditDescription(auctionDb);
    }

    public List<Auction> GetAllActiveAuctions()
    {
        List<AuctionDb> auctionDbs = _auctionRepository.GetAllActiveAuctions();
        List<Auction> auctions = new List<Auction>();
        
        foreach (AuctionDb auctionDb in auctionDbs)
        {
            auctions.Add(_mapper.Map<Auction>(auctionDb));
        }
        
        return auctions;
    }

    public Auction GetDetails(Auction auction) 
    {
        // Steg 1: Mappa Auction till AuctionDb och skriv ut auktionens information
        AuctionDb auctionDb = _mapper.Map<AuctionDb>(auction);

        // Steg 2: Hämta detaljer om auktionen från databasen och skriv ut buden
        auctionDb = _auctionRepository.GetDetails(auctionDb);
        auction = _mapper.Map<Auction>(auctionDb);

        foreach (BidDb bidDb in auctionDb.BidDbs)
        {
            Bid bid = _mapper.Map<Bid>(bidDb);
            auction.AddBid(bid);
        }

        return auction;
    }


    public List<Auction> GetAllBiddedAuctions(string userName)
    {
        List<AuctionDb> auctionDbs = _auctionRepository.GetAllBiddedAuctions(userName);
        List<Auction> auctions = new List<Auction>();
        
        foreach (AuctionDb auctionDb in auctionDbs)
        {
            auctions.Add(_mapper.Map<Auction>(auctionDb));
        }
        
        return auctions;
    }

    public List<Auction> GetAllWonAuctions(string userName)
    {
        List<AuctionDb> auctionDbs = _auctionRepository.GetAllWonAuctions(userName);
        List<Auction> auctions = new List<Auction>();
        
        foreach (AuctionDb auctionDb in auctionDbs)
        {
            auctions.Add(_mapper.Map<Auction>(auctionDb));
        }
        
        return auctions;
    }
}