using System.ComponentModel.DataAnnotations;
using AuctionApp.Core;

namespace AuctionApp.Models.Auctions;

public class BidVm
{
    [ScaffoldColumn(false)]
    public int Id { get; set; }
    
    public int Price  { get; set; }
    
    public string UserName  { get; set; }

    public static BidVm FromBid(Bid bid)
    {
        return new BidVm()
        {
            Id = bid.Id,
            Price = bid.Price,
            UserName = bid.UserName
        };
    }
}