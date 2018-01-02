using Microsoft.EntityFrameworkCore;
using AuctionApp.Models;

namespace AuctionApp.Models
{
    public class MyDbContext: DbContext
    {
        public MyDbContext(DbContextOptions options) : base(options){}
        public DbSet<User> users {get; set;}
        public DbSet<Auction> auctions {get; set;}
        public DbSet<Bid> bids {get; set;}

        
            

    }
}