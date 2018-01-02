using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AuctionApp.Models
{
    public class Bid
    {
        [Key]
        public long bid_id {get;set;}

        public DateTime created_at {get;set;}
        public decimal bidAmount {get;set;}
        public long user_id {get;set;}
        public long auction_id {get;set;}
        
        public User Bidder {get;set;}
        public Auction AuctionInBid {get;set;}

        public Bid()
        {
            created_at = DateTime.Now;

        }
    }
}