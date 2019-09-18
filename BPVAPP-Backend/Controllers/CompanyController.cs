using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BPVAPP_Backend.Database;
using BPVAPP_Backend.Database.Models;

namespace BPVAPP_Backend.Controllers
{
    public class CompanyController : Controller
    {
        [HttpPost]
        public IActionResult CreateCompany([FromBody]Company model)
        {
            var db = new DbConnection();
            db.AddModel(model);
            return View();
        }
    }
}