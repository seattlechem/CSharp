using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Http;

namespace JsonAjax.Controllers
{
    public class HomeController: Controller
    {
        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("/random")]
        public JsonResult Random()
        {
            int internalCounter;
            string source = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random rand = new Random();
            if (HttpContext.Session.GetInt32("counter") == null)
            {
                HttpContext.Session.SetInt32("counter", 0);
            }
            else
            {
                internalCounter = (int)HttpContext.Session.GetInt32("counter");
                internalCounter += 1;
                HttpContext.Session.SetInt32("counter", internalCounter);
            }

            string output = "";
            for (int i=0; i<14; i++)
            {
                output += source[(int)rand.Next(0, source.Length)];
                
            }
            
            var result = new
            {
                Counter = HttpContext.Session.GetInt32("counter"),
                Output = output
            };

            return Json(result);
        }

    }

}