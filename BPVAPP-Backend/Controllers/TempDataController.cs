using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Mvc;

namespace BPVAPP_Backend.Controllers
{
    [Route("api/[controller]")]
    public class TempDataController : Controller
    {

        [HttpGet]
        public string Get()
        {
            var data = System.IO.File.ReadAllText("data.json");
            return data;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}