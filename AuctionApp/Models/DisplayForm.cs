using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AuctionApp.Models;

namespace AuctionApp.Models
{
    public class DisplayForm
    {
        public List<Auction> Auctions {get;set;}

        public List<User> Users {get;set;}
        public User User {get;set;}
        public List<Bid> Bids {get;set;}

        [Required]
        [DataType(DataType.Currency)]
        public decimal bitAmount {get;set;}
    }
}