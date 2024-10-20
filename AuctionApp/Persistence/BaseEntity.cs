namespace AuctionApp.Persistence;

using System.ComponentModel.DataAnnotations;

public class BaseEntity
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public string UserName { get; set; }
}