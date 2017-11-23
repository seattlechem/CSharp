using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Dojosurvey.Controllers
{
    
    public class HomeController: Controller
    {
    
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            int? numTimes = HttpContext.Session.GetInt32("Times");

            if(numTimes == null)
            {
                HttpContext.Session.SetInt32("Times", 0);

            }
           

            ViewBag.Times = HttpContext.Session.GetInt32("Times");

            List<string> errors = new List<string>();
            if(errors != null)
            {
                foreach(string error in errors)
                {
                    ViewBag.Errors += error;
                }

            }


            return View();
        }

        [HttpPost]
        [Route("formprocess")]
        public IActionResult formprocess(string name, string DojoLocation, string favoriteLanguage, string commentBox)
        {   
            int? numTimes = HttpContext.Session.GetInt32("Times");
            //don't need to check if numTimes is null or not because index is checking this.
            numTimes++;
            ViewBag.Errors = new List<string>();
            
            HttpContext.Session.SetInt32("Times", (int)numTimes);


            if(name == null)
            {
                ViewBag.Errors.Add("Please enter name");
        
            }
            else
            {
                HttpContext.Session.SetString("Name", name);
            }
            HttpContext.Session.SetString("Location", DojoLocation);
            HttpContext.Session.SetString("Language", favoriteLanguage);
            HttpContext.Session.SetString("Comment", commentBox);

            return RedirectToAction("ShowResults");
            
        }

        [Route("ShowResults")]
        public IActionResult ShowResults()
        {
        
            ViewBag.Name = HttpContext.Session.GetString("Name");
            ViewBag.Location = HttpContext.Session.GetString("Location");
            ViewBag.Language = HttpContext.Session.GetString("Language");
            ViewBag.Comment = HttpContext.Session.GetString("Comment");
            ViewBag.Times = HttpContext.Session.GetInt32("Times");
            return View();
        }


        
    }
}