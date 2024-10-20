using System.ComponentModel.DataAnnotations;
using AuctionApp.Core;

namespace AuctionApp.Models.Auctions;

public class AuctionDetailsVm
{
    [ScaffoldColumn(false)]
    public int Id { get; set; }
    
    public string Title { get; set; }
    
    public string Description { get; set; }
    
    [Display(Name = "End date")]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
    public DateTime EndDate { get; set; }
    
    public int Price { get; set; }
    
    public bool IsCompleted { get; set; }

    public List<BidVm> BidVms { get; set; } = new();
    
    public bool IsOwner { get; set; } // Ny egenskap för att ange om användaren är ägare
    
    public string UserName { get; set; } 

    public static AuctionDetailsVm FromAuction(Auction auction, bool isOwner)
    {
        var detailsVm = new AuctionDetailsVm()
        {
            Id = auction.Id,
            Title = auction.Title,
            Description = auction.Description,
            EndDate = auction.EndDate,
            Price = auction.Price,
            IsCompleted = auction.IsCompleted(),
            IsOwner = isOwner,
            UserName = auction.UserName
        };
        
        // Sortera buden i fallande ordning efter pris innan de läggs till i BidVms-listan
        foreach (var bid in auction.Bids.OrderByDescending(bid => bid.Price)) 
        {
            detailsVm.BidVms.Add(BidVm.FromBid(bid));
        }
        
        return detailsVm;
    }
}