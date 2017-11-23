using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Http;

namespace RandomPasscode.Controllers
{
    public class HomeController: Controller
    {
        [HttpGet("")]
        public IActionResult Index()
        {
            int resultCounter;
            if(HttpContext.Session.GetString("RandomString")==null)
            {
                HttpContext.Session.SetString("RandomString", "");
            }
            else
            {
                TempData["message"] = HttpContext.Session.GetString("RandomString");
            }

            if(HttpContext.Session.GetInt32("counter") == null)
            {
                HttpContext.Session.SetInt32("counter", 0);
            }
            else
            {
                resultCounter = (int)HttpContext.Session.GetInt32("counter");
                resultCounter += 1;
                HttpContext.Session.SetInt32("counter", resultCounter);
                TempData["counter"] = HttpContext.Session.GetInt32("counter");

            }
            return View();
        }

        [HttpGet("random")]
        public IActionResult Random()
        {
            Random rand = new Random();
            string randString = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string generatedString ="";
            //string length 14
            for(int i=0; i<14; i++)
            {
                generatedString += randString[rand.Next(0, randString.Length)];

            }
            HttpContext.Session.SetString("RandomString", generatedString);

            
            return RedirectToAction("Index");
        }
    }
}