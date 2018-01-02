using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Linq;
using AuctionApp.Models;
using Microsoft.AspNetCore.Identity;

namespace AuctionApp.Controllers
{
    public class HomeController : Controller
    {
        private MyDbContext _context;
        public HomeController(MyDbContext context)
        {
            _context = context;
        }
        
        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
        
            return View();
        }

        [HttpPost("create")]
        public IActionResult Create(newUser user)
        {
            PasswordHasher<User> hasher = new PasswordHasher<User>();
            //check uniquness of username
            if(_context.users.Where(u => u.userName == user.userName)
                             .ToList()
                             .Count() > 0)
            {
                ModelState.AddModelError("userName", "Username already exists");
            }

            else if(ModelState.IsValid)
            {
                User toCreate = new User()
                {
                    first_name = user.first_name,
                    last_name = user.last_name,
                    userName = user.userName,
                    password = hasher.HashPassword(user, user.password)
                };

                _context.users.Add(toCreate);
                _context.SaveChanges();

                

                return RedirectToAction("Index", "Home");
            }
           
            return View("index");
        }

        [HttpPost("login")]
        public IActionResult Login(LoginUser user)
        {
            //check if username exists
            if(_context.users.Where(u => u.userName == user.logUserName)
                             .ToList()
                             .Count() == 0)  
            {
                ModelState.AddModelError("logUserName", "Username does not exist");
            }
            else if(ModelState.IsValid)
            {
                //check if password is correct
                User loggedUser = _context.users.SingleOrDefault(u =>u.userName == user.logUserName);
                var Hasher = new PasswordHasher<User>();
                if(0 != Hasher.VerifyHashedPassword(loggedUser, loggedUser.password, user.logPassword)) 
                {
                    HttpContext.Session.SetInt32("id", (int)loggedUser.user_id);
                    return RedirectToAction("Main", "Auction");
                }
                else
                {
                    ModelState.AddModelError("logPassword", "Email/Password is incorrect");
                }
            }
           
            return View("index");
        }


        
    }    
}
