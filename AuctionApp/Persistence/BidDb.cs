using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuctionApp.Persistence;

public class BidDb : BaseEntity
{
    [Required]
    public int Price { get; set; }
    
    [Required]
    [DataType(DataType.DateTime)]
    public DateTime BidDate { get; set; }

    // FK and navigation propert
    [ForeignKey("AuctionId")]
    public AuctionDb AuctionDb { get; set; }
    
    public int AuctionId { get; set; } 
}