using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuctionApp.Persistence;

public class BidDb
{
    [Key]
    public int? Id { get; set; }

    [Required]
    public int? Price { get; set; }
    
    [Required]
    [DataType(DataType.DateTime)]
    public DateTime? BidDate { get; set; }
    
    [Required]
    public string? UserName { get; set; }

    // FK and navigation propert
    [ForeignKey("AuctionId")]
    public AuctionDb? AuctionDb { get; set; }
    
    public int AuctionId { get; set; } 
}