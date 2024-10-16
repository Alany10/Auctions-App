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

    public static AuctionDetailsVm FromAuction(Auction auction)
    {
        var detailsVm = new AuctionDetailsVm()
        {
            Id = auction.Id,
            Title = auction.Title,
            Description = auction.Description,
            EndDate = auction.EndDate,
            Price = auction.Price,
            IsCompleted = auction.IsCompleted(),
        };

        foreach (var bid in auction.Bids)
        {
            detailsVm.BidVms.Add(BidVm.FromBid(bid));
        }
        
        return detailsVm;
    }
}