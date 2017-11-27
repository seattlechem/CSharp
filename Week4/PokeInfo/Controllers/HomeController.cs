using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace PokeInfo.Controllers
{
    public class HomeController : Controller
    {
        // GET: /Home/
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Index(int id)
        {
            Dictionary<string, object> PokeData;
            await WebRequest.GetPokemonDataAsync(id, data =>{
                PokeData = data;
                ViewBag.Pokemon = PokeData;
            });
            
            return View();
        }
    }
}
