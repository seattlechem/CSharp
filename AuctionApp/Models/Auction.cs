
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AuctionApp.Models
{
    public class Auction
    {
        [Key]
        public long auction_id {get;set;}

        [Required]
        [Display(Name="Product Name")]
        [MinLength(3)]
        public string productName {get;set;}

        [Required]
        [Display(Name="Description")]
        [MinLength(10)]
        public string description {get;set;}

        [DataType(DataType.Currency)]
        [Display(Name="Starting Bid")]
        public decimal startingBid {get;set;}

        [DataType(DataType.Date)]
        [CheckDateRange]
        [Display(Name="End Date")]
        public DateTime endDate {get;set;}

        public DateTime created_at {get;set;}

        public DateTime updated_at {get;set;}
        

        public long user_id {get;set;}

        public List<Bid> ListBidUsers {get;set;}

        public Auction()
        {
            created_at = DateTime.Now;
            updated_at = DateTime.Now;
        }




        
    }
}