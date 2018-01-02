using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Linq;
using AuctionApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AuctionApp.Controllers
{
    public class AuctionController : Controller
    {
        private MyDbContext _context;
        public AuctionController(MyDbContext context)
        {
            _context = context;
        }

        [HttpGet("main")]
        public IActionResult Main()
        {
            
            int? loggedUserId = HttpContext.Session.GetInt32("id");
            if(loggedUserId == null)
            {
                ViewBag.Status = "Please log in first.";
                return RedirectToAction("Index", "Home");
            } 

            // if there's no current auctions, direct user to "add" page
            if(_context.auctions.ToList().Count() == 0)
            {
                return RedirectToAction("Add", "Auction");
            }

            AuctionIndex newAuctionIndex = new AuctionIndex()
            {
                Users = _context.users.ToList(),
                User = _context.users
                                    .Include(u => u.ListBidAuctions)
                                    .SingleOrDefault(u => u.user_id == loggedUserId),
                
                Auctions = _context.auctions.Include(a => a.ListBidUsers).OrderBy(k => k.endDate).ToList(),
                Bids = _context.bids.OrderByDescending(b => b.bidAmount).ToList()
                

            };

            
            
            if(_context.auctions.Where(a => a.endDate < DateTime.Today).Count() > 0)
            {
                //transfer winning bid to seller
                //subtract winning bid from top bidder
                //check the highest bid & highest bidder
                //check seller
                
                var AuctionModel = _context.auctions.Include(a => a.ListBidUsers).ToList();
                var UserModel = _context.users.Include(u => u.ListBidAuctions).FirstOrDefault();

                //there can be no bidders
                var AuctionToEnd = AuctionModel.Where(a => a.endDate < DateTime.Today).ToList();
                
                // var AuctionToEnd = _context.auctions.Where(a => a.endDate < DateTime.Today);
                // var testModel = _context.users.Include(u => u.ListBidAuctions).FirstOrDefault();
                // var testModel2 = _context.bids.Include(b => b.AuctionInBid).Include(c => c.Bidder).FirstOrDefault();
                // var answer = testModel.ListBidAuctions.Where(l => l.auction_id == 18).FirstOrDefault();
                
        
                foreach(var auctionToEnd in AuctionToEnd)
                {
                    //if there is no bidder, winngBidder becomes null object.

                    //if there is bidder

                    var endingAuctionID = auctionToEnd.auction_id;

                    if(UserModel.ListBidAuctions.FirstOrDefault(g => g.auction_id == endingAuctionID) == null)
                    {
                        //no money transfer //just delete the auction //delete auction, auctionToEnd

                        _context.auctions.Remove(auctionToEnd);
                        _context.SaveChanges();

                        return View(newAuctionIndex); 
     
                    }
                    // winning bid
                    // var winningBid = _context.bids.Where(b => b.auction_id == endingAuctionID).OrderByDescending(o => o.bidAmount).FirstOrDefault();
                    
                    
                    // winning bidder
                    // var winningBidder = _context.users.FirstOrDefault(u => u.user_id == winningBid.user_id);
                    
                    else
                    {
                        var winningBid = UserModel.ListBidAuctions.OrderByDescending(l => l.bidAmount).FirstOrDefault();
                        var winningBidder = UserModel.ListBidAuctions.FirstOrDefault(f => f.auction_id == endingAuctionID).Bidder;
                        var sellerOfWinningAuction = UserModel.ListBidAuctions.FirstOrDefault(h => h.auction_id == endingAuctionID).Bidder;

                        //subtract winning bid amount from winning bidder
                        winningBidder.wallet -= winningBid.bidAmount;
                        _context.SaveChanges();


                        //transfer winning bid amount to winner
                        sellerOfWinningAuction.wallet += winningBid.bidAmount;
                        _context.SaveChanges();

                        //delete this auction from db
                        _context.bids.Remove(winningBid);
                        _context.SaveChanges();

                        _context.auctions.Remove(auctionToEnd);
                        _context.SaveChanges();
                        
                        return View(newAuctionIndex); 

                    }
                    
                    

                }
                
                return View(newAuctionIndex);
            }
             
        return View(newAuctionIndex);   
        }
        
        [HttpGet("add")]
        
        public IActionResult Add()
        {
            int? loggedUserId = HttpContext.Session.GetInt32("id");
            if(loggedUserId == null)
            {
                ViewBag.Status = "Please log in first.";
                return RedirectToAction("Index", "Home");
            }
            ViewBag.UserId = loggedUserId;
            return View();
        }

        [HttpPost("createnewauction")]
        public IActionResult CreateNewAuction(Auction newAuction)
        {
           if(ModelState.IsValid)
            {
                Auction AuctionToCreate = new Auction()
                {
                    productName = newAuction.productName,
                    startingBid = newAuction.startingBid,
                    description = newAuction.description,
                    endDate = newAuction.endDate,
                    user_id = newAuction.user_id
                };

                _context.auctions.Add(AuctionToCreate);
                _context.SaveChanges();

                return RedirectToAction("Main", "Auction");
            }
           
            return View("Add");
        }

        [HttpGet("display/{auction_id}")]
        public IActionResult Display(int auction_id)
        {
            int? loggedUserId = HttpContext.Session.GetInt32("id");
            
            if(loggedUserId == null)
            {
                ViewBag.Status = "Please log in first.";
                return RedirectToAction("Index", "Home");
            }
            
            //store auction_id into session
            HttpContext.Session.SetInt32("auction_id", auction_id);

            AuctionIndex newAuctionIndex = new AuctionIndex()
            {
                Users = _context.users.ToList(),
                User = _context.users
                                    .Include(u => u.ListBidAuctions)
                                    .SingleOrDefault(u => u.user_id == loggedUserId),
                Auctions = _context.auctions.Where(a => a.auction_id == auction_id).ToList(),
                Bids = _context.bids.Where(b => b.auction_id == auction_id).OrderByDescending(o => o.bidAmount).ToList()
                


            };
            
            return View(newAuctionIndex);
        }

        [HttpGet("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost("bid")]
        public IActionResult Bid(AuctionIndex formAuction)
        {
            //user_id from session
            //auction_id from session
            //bidAmount from formAuction.bidAmount
           long bidUserId = Convert.ToInt64(HttpContext.Session.GetInt32("id"));
           long bidAuctionId = Convert.ToInt64(HttpContext.Session.GetInt32("auction_id"));
           decimal BidAmount = formAuction.BidAmount;

           //check user's wallet
           decimal balance = _context.users.SingleOrDefault(u => u.user_id == bidUserId).wallet;

           //check previous max bid amount
           decimal previousMaxAmt;
           if(_context.bids.Where(b => b.auction_id == bidAuctionId)
                                    .OrderByDescending(o => o.bidAmount).Count() != 0)
            {
                previousMaxAmt = _context.bids.Where(b => b.auction_id == bidAuctionId)
                                    .OrderByDescending(o => o.bidAmount).FirstOrDefault().bidAmount;
            }
           
           else
           {
               //set it to starting Bid
               previousMaxAmt = _context.auctions.Where(b => b.auction_id == bidAuctionId)
                                .FirstOrDefault().startingBid;
           }

            
            if(ModelState.IsValid & BidAmount < balance & BidAmount > previousMaxAmt)
            {

                //Add new bid
                Bid bidToAdd = new Bid()
                {
                    bidAmount = formAuction.BidAmount,
                    user_id = bidUserId,
                    auction_id = bidAuctionId

                };

                _context.bids.Add(bidToAdd);
                _context.SaveChanges();
        
                return RedirectToAction("Main", "Auction");
            }

            return RedirectToAction("Main");
    
        }

        [HttpGet("delete/{auction_id}")]
        public IActionResult Delete(int auction_id)
        {
            int? loggedUserId = HttpContext.Session.GetInt32("id");
            
            if(loggedUserId == null)
            {
                ViewBag.Status = "Please log in first.";
                return RedirectToAction("Index", "Home");
            }

            List<Bid> bidToRemove = _context.bids.Where(a => a.auction_id == auction_id).ToList();
            
            if(bidToRemove != null)
            {
               foreach(var remove in bidToRemove)
                {
                    _context.bids.Remove(remove);
                    _context.SaveChanges();
                }
            }

            List<Auction> auctionToRemove = _context.auctions.Where(a => a.auction_id == auction_id).ToList();

            if (auctionToRemove != null)
            {
                foreach(var remove in auctionToRemove)
                {
                    _context.auctions.Remove(remove);
                    _context.SaveChanges();
                }

                
            }    


            return RedirectToAction("Main");
        }
    }
}