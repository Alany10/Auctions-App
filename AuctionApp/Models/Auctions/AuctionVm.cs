using System.ComponentModel.DataAnnotations;
using AuctionApp.Core;

namespace AuctionApp.Models.Auctions;

public class AuctionVm
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

    public string UserName { get; set; }

    public static AuctionVm fromAuction(Auction auction)
    {
        return new AuctionVm()
        {
            Id = auction.Id,
            Title = auction.Title,
            Description = auction.Description,
            EndDate = auction.EndDate,
            Price = auction.Price,
            IsCompleted = auction.IsCompleted(),
            UserName = auction.UserName
        };
    }
}