using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace timedisplay.Controllers
{  
    public class TimeDisplayController: Controller
    {
        [HttpGet]
        [Route("")]
        public IActionResult index()
        {
            return View();
        }
    }    


    
}