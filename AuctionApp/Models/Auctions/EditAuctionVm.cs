namespace AuctionApp.Models.Auctions;

using System.ComponentModel.DataAnnotations;

public class EditAuctionVm
{
    [Required(ErrorMessage = "Description is required.")]
    public string Description { get; set; }
}