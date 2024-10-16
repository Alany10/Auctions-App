using System.Data;
using AuctionApp.Core;
using AuctionApp.Core.Interfaces;
using AuctionApp.Models.Auctions;
using Microsoft.AspNetCore.Mvc;

namespace AuctionApp.Controllers
{
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
            List<Auction> auctions = _auctionService.GetAllByUserName("admin");
            List<AuctionVm> auctionsVMs = new List<AuctionVm>();

            foreach (Auction auction in auctions)
            {
                auctionsVMs.Add(AuctionVm.fromAuction(auction));
            }
            
            return View(auctionsVMs);
        }

        // GET: AuctionsController/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                Auction auction = _auctionService.GetById(id, "admin");
                
                AuctionDetailsVm detailsVm = AuctionDetailsVm.FromAuction(auction);
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
                    string userName = "admin";
                    
                    _auctionService.Add(title, description, endDate, price, userName);
                    return RedirectToAction("Index");
                }
                return View(createAuctionVm);
            }
            catch(DataException ex)
            {
                return View(createAuctionVm);
            }
        }

        /*
        // GET: AuctionsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AuctionsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AuctionsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AuctionsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        
        */
    }
}
