namespace AuctionApp.Models.Auctions;

using System.ComponentModel.DataAnnotations;

public class CreateBidVm
{
    [Required(ErrorMessage = "Price is required.")]
    public int Price { get; set; }
}