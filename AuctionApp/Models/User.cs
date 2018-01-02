using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AuctionApp.Models
{
    public class User
    {
        [Key]
        public long user_id {get;set;}

        [Required]
        [Display(Name="First Name")]
        [MinLength(2)]
        public string first_name {get;set;}

        [Required]
        [Display(Name="Last Name")]
        [MinLength(2)]
        public string last_name {get;set;}
        
        [Required]
        [Display(Name="Username")]
        public string userName {get;set;}

        [Required]
        [Display(Name="Password")]
        [DataType(DataType.Password)]
        public string password {get;set;}

        [DataType(DataType.Currency)]
        [Display(Name="Wallet")]
        public decimal wallet {get;set;}


        public DateTime created_at {get;set;}

        public DateTime updated_at {get;set;}
        
        
        public List<Bid> ListBidAuctions {get;set;}

        public User()
        {
            created_at = DateTime.Now;
            updated_at = DateTime.Now;
            wallet = 1000;

        }
        
        
    }

    public class newUser: User
    {
        [Required]
        [Display(Name="Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("password", ErrorMessage="Password does not match")]
        public string confirm {get;set;}
        
    }

    public class LoginUser
    {
        [Required]
        [Display(Name="Username")]
        public string logUserName {get;set;}

        [Required]
        [Display(Name="Password")]
        [DataType(DataType.Password)]
        public string logPassword {get;set;}

    }
}