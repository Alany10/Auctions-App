namespace AuctionApp.Core;

public class Bid
{
    public int Id { get; set; }

    private int _price;
    public int Price
    {
        get => _price;
        set
        {
            if (value < 1)
            {
                _price = 1; // Sätter priset till minst 1 om ett lägre värde anges
            }
            else
            {
                _price = value; // Annars används det angivna värdet
            }
        }
    }
    
    // public Auction Auction { get; set; }
    
    public string UserName { get; set; }

    public Bid(int price, string userName)
    {
        _price = price;
        // Auction = auction;
        UserName = userName;
    }

    public Bid(int id, int price, string userName) 
    {
        Id = id;
        _price = price;
        //Auction = auction;
        UserName = userName;
    }

    public Bid() { }

    public override string ToString()
    {
        return $"{Id}, price: {Price}";
    }
}