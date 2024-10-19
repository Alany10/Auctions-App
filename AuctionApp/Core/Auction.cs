namespace AuctionApp.Core
{
    public class Auction
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        private DateTime _endDate;
        public DateTime EndDate
        {
            get => _endDate;
            set
            {
                if (value.Date < DateTime.Now.Date)
                {
                    _endDate = DateTime.Now.Date.AddDays(7); // Sätt slutdatum till en vecka från idag om ett tidigare datum anges
                }
                else
                {
                    _endDate = value;
                }
            }
        }

        private int _price;
        public int Price
        {
            get => _price;
            set
            {
                if (value < 1)
                {
                    _price = 1; // Sätt priset till minst 1 om ett lägre värde anges
                }
                else
                {
                    _price = value;
                }
            }
        }

        public string UserName { get; set; }

        private List<Bid> _bids = new List<Bid>();
        public IEnumerable<Bid> Bids => _bids;

        public Auction(string title, string description, DateTime endDate, int price, string userName)
        {
            Title = title;
            Description = description;
            EndDate = endDate; // Använder property för att säkerställa validering
            Price = price;     // Använder property för att säkerställa validering
            UserName = userName;
        }

        public Auction(int id, string title, string description, DateTime endDate, int price, string userName)
        {
            Id = id;
            Title = title;
            Description = description;
            EndDate = endDate; // Använder property för att säkerställa validering
            Price = price;     // Använder property för att säkerställa validering
            UserName = userName;
        }

        public Auction() { }

        public bool AddBid(Bid bid)
        {
            if (bid.Price > Price)
            {
                Price = bid.Price;
            }
            
            _bids.Add(bid);
            return true;
        }

        public bool IsCompleted()
        {
            return EndDate > DateTime.Now;
        }

        public static bool IsEqual(Auction auction1, Auction auction2)
        {
            return auction1.Title.Equals(auction2.Title )
                   && auction1.Description.Equals(auction2.Description )  
                   && auction1.EndDate == auction2.EndDate 
                   && auction1.Price == auction2.Price 
                   && auction1.UserName.Equals(auction2.UserName);
        }

        public override string ToString()
        {
            return $"{Id}, {Title}, {Description}, End date: {EndDate}, Price: {Price}";
        }
    }
}
