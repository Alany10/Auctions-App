namespace AuctionApp.Models.Auctions;

using System.ComponentModel.DataAnnotations;

public class CreateAuctionVm 
{
    [Required(ErrorMessage = "Title is required.")]
    [StringLength(100, ErrorMessage = "Max length 100 characters")]
    public string Title { get; set; }

    [Required(ErrorMessage = "Description is required.")]
    public string Description { get; set; }

    [Required(ErrorMessage = "End date is required.")]
    [DataType(DataType.DateTime)]
    [FutureDate] // Använd den anpassade attributen här
    public DateTime EndDate { get; set; }

    [Required(ErrorMessage = "Price is required.")]
    public int Price { get; set; }

    // Definiera den anpassade valideringsattributen inuti klassen
    public class FutureDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime dateTimeValue)
            {
                if (dateTimeValue <= DateTime.Now)
                {
                    return new ValidationResult("The date must be in the future.");
                }
            }
            return ValidationResult.Success;
        }
    }
}
