using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BPVAPP_Backend.Database;
using BPVAPP_Backend.Database.Models;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Authorization;

namespace BPVAPP_Backend.Controllers
{
    [Route("api/[controller]")]
    public class CompanyController : Controller
    {
        [HttpPost]

        public IActionResult CreateCompany([FromBody]CompanyModel model)
        {
            var db = new DbConnection();
            db.AddModel(model);
            return View();
        }

        [HttpGet]
        [Route("companyId/{id}")]
        public object GetCompanyById(int id)
        {
            try
            {
                JObject json = (JObject)JsonConvert.DeserializeObject(System.IO.File.ReadAllText("data.json"));

                var rtn = json["Bedrijf"][id];

                JObject company = new JObject();
                JArray companyarr = new JArray
                {
                    json["Bedrijf"][id]
                };
                company["Bedrijf"] = companyarr;

                return company.ToString();
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }
    }
}