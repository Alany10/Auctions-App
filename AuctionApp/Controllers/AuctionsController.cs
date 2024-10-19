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
        public ActionResult Index()
        {
            List<Auction> auctions = _auctionService.GetAllByUserName(User.Identity.Name);
            List<AuctionVm> auctionsVMs = new List<AuctionVm>();

            foreach (Auction auction in auctions)
            {
                auctionsVMs.Add(AuctionVm.fromAuction(auction));
            }

            return View(auctionsVMs);
        }

        // Ny metod för att hämta alla auktioner oavsett ägare
        public ActionResult IndexAll()
        {
            // Hämta alla auktioner
            List<Auction>
                auctions = _auctionService.ListAllAuctions(); // Du behöver implementera denna metod i din tjänst
            List<AuctionVm> auctionsVMs = new List<AuctionVm>();

            foreach (Auction auction in auctions)
            {
                auctionsVMs.Add(AuctionVm.fromAuction(auction));
            }

            return View("Index", auctionsVMs); // Använd samma vy som för Index
        }
        
        public ActionResult BiddedAuctions()
        {
            string userName = User.Identity.Name;
            
            // Hämta alla auktioner
            List<Auction>
                auctions = _auctionService.ListAllyourAuctions(userName); // Du behöver implementera denna metod i din tjänst
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
                Auction auction = _auctionService.GetById(id);
                bool isOwner = _auctionService.IsOwner(auction.Id, User.Identity.Name);

                AuctionDetailsVm detailsVm = AuctionDetailsVm.FromAuction(auction, isOwner);
                return View(detailsVm);
            }
            catch (Exception e)
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

                    _auctionService.Add(title, description, endDate, price, userName);
                    return RedirectToAction("Index");
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
            Auction auction = _auctionService.GetById(id);

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

                    _auctionService.editDescription(id, description, userName);
                    return RedirectToAction("Index");
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
                    return RedirectToAction("Index");
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
