using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Http;

namespace NinjaGold.Controllers
{
    public class HomeController: Controller
    {
        [HttpGet("")]
        public IActionResult Index()
        {
            if(HttpContext.Session.GetInt32("TotalGold")== null)
            {
                HttpContext.Session.SetInt32("TotalGold", 0);
            }

            TempData["message"]=$"{HttpContext.Session.GetInt32("TotalGold")}";

            return View();
        }

        [HttpGet("GetGold/{building}")]
        public IActionResult Gold(string building)
        {   
            HttpContext.Session.SetInt32("TotalGold", (int)HttpContext.Session.GetInt32("TotalGold")+(int)NumberOfGold(building));
   
            return RedirectToAction("Index");
        }

        private int NumberOfGold(string building)
        {   
            Random rand = new Random();
            switch(building)
            {
                case "farm":
                    return rand.Next(3,11);
                case "house":
                    return rand.Next(10,21);
                case "cave":
                    return rand.Next(0,15);
                default:
                    return rand.Next(-50,51);
    
            }
        }
    }
}