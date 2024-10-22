using System.ComponentModel.DataAnnotations;

namespace AuctionApp.Persistence;

public class AuctionDb : BaseEntity
{
    [Required]
    [MaxLength(100)]
    public string Title { get; set; }

    [Required]
    public string Description { get; set; }

    [Required] 
    [DataType(DataType.DateTime)]
    public DateTime EndDate { get; set; }

    [Required] 
    public int Price { get; set; }
    
    // Navigation property
    public List<BidDb> BidDbs { get; set; } = new List<BidDb>();
}