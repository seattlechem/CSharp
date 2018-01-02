using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AuctionApp.Models;

namespace AuctionApp.Models
{
    public class AuctionIndex
    {
        public List<Auction> Auctions {get;set;}

        public List<User> Users {get;set;}
        public User User {get;set;}
        public List<Bid> Bids {get;set;}
        
        [Range(0, double.MaxValue, ErrorMessage = "Please enter valid doubleNumber")]
        public long BidAmount {get;set;}

    }
}