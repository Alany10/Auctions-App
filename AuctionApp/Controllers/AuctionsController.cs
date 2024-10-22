using System.Data;
using AuctionApp.Core;
using AuctionApp.Core.Interfaces;
using AuctionApp.Models.Auctions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuctionApp.Controllers
{
    [Authorize]
    public class AuctionsController : Controller
    {
        private IAuctionService _auctionService;

        public AuctionsController(IAuctionService auctionService)
        {
            _auctionService = auctionService;
        }

        // GET: AuctionsController
        public ActionResult YourAuctions()
        {
            List<Auction> auctions = _auctionService.ListYourAuctions(User.Identity.Name);
            List<AuctionVm> auctionsVMs = new List<AuctionVm>();

            foreach (Auction auction in auctions)
            {
                auctionsVMs.Add(AuctionVm.fromAuction(auction));
            }

            return View("index", auctionsVMs);
        }

        
        public ActionResult AllAuctions()
        {
            List<Auction>
                auctions = _auctionService.ListAllActiveAuctions(); 
            List<AuctionVm> auctionsVMs = new List<AuctionVm>();

            foreach (Auction auction in auctions)
            {
                auctionsVMs.Add(AuctionVm.fromAuction(auction));
            }

            return View("Index", auctionsVMs);
        }
        
        public ActionResult BiddedAuctions()
        {
            string userName = User.Identity.Name;
            
            // Hämta alla auktioner
            List<Auction>
                auctions = _auctionService.ListBiddedAuctions(userName); // Du behöver implementera denna metod i din tjänst
            List<AuctionVm> auctionsVMs = new List<AuctionVm>();

            foreach (Auction auction in auctions)
            {
                auctionsVMs.Add(AuctionVm.fromAuction(auction));
            }

            return View("Index", auctionsVMs); // Använd samma vy som för Index
        }
        
        public ActionResult WonAuctions()
        {
            string userName = User.Identity.Name;
            
            // Hämta alla auktioner
            List<Auction>
                auctions = _auctionService.ListAllWonAuctions(userName); // Du behöver implementera denna metod i din tjänst
            List<AuctionVm> auctionsVMs = new List<AuctionVm>();

            foreach (Auction auction in auctions)
            {
                auctionsVMs.Add(AuctionVm.fromAuction(auction));
            }

            return View("Index", auctionsVMs); // Använd samma vy som för Index
        }

        // GET: AuctionsController/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                Auction auction = _auctionService.GetAuctionById(id);
                auction = _auctionService.GetDetails(auction);
                bool isOwner = _auctionService.IsOwner(auction.Id, User.Identity.Name);

                AuctionDetailsVm detailsVm = AuctionDetailsVm.FromAuction(auction, isOwner);
                return View(detailsVm);
            }
            catch (DataException e)
            {
                return BadRequest();
            }
        }

        // GET: AuctionsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AuctionsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateAuctionVm createAuctionVm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string title = createAuctionVm.Title;
                    string description = createAuctionVm.Description;
                    DateTime endDate = createAuctionVm.EndDate;
                    int price = createAuctionVm.Price;
                    string userName = User.Identity.Name;

                    _auctionService.CreateAuction(title, description, endDate, price, userName);
                    return RedirectToAction("YourAuctions");
                }

                return View(createAuctionVm);
            }
            catch (DataException ex)
            {
                return View(createAuctionVm);
            }
        }

        // GET: AuctionsController/Edit/5
        public ActionResult Edit(int id)
        {
            // Hämta auktionen från databasen med hjälp av id
            Auction auction = _auctionService.GetAuctionById(id);

            if (auction == null) return NotFound();

            // check if current user "owns" this auction
            if (!auction.UserName.Equals(User.Identity.Name)) return Unauthorized();

            // Skapa en ViewModel och fyll i värdet från den befintliga auktionen
            EditAuctionVm editAuctionVm = new EditAuctionVm
            {
                Description = auction.Description
            };

            // Skicka ViewModel till vyn
            return View(editAuctionVm);
        }

        // POST: AuctionsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EditAuctionVm editAuctionVm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string description = editAuctionVm.Description;
                    string userName = User.Identity.Name;

                    _auctionService.EditDescription(id, description, userName);
                    return RedirectToAction("YourAuctions");
                }

                return View(editAuctionVm);
            }
            catch (DataException ex)
            {
                return View(editAuctionVm);
            }
        }
        
        public ActionResult Bid()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Bid(CreateBidVm createBidVm, int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int price = createBidVm.Price;

                    string userName = User.Identity.Name;
                    
                    _auctionService.Bid(price, id, userName);
                    return RedirectToAction("BiddedAuctions");
                }

                return View(createBidVm);
            }
            catch (DataException ex)
            {
                return View(createBidVm);
            }
        }
    }
}
