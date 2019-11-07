using Microsoft.AspNetCore.Mvc;
using BPVAPP_Backend.Database.Models;
using BPVAPP_Backend.Response;
using System;
using System.Collections.Generic;

namespace BPVAPP_Backend.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        public object Get()
        {
            var compannies = new List<CompanyModel>();

            for (var i = 0; i < 5; i++)
            {
                compannies.Add(new CompanyModel {
                    Bedrijfsnaam = $"Bedrijf_{i}",
                    Adres = $"Adres_{i}",
                    Plaats = $"Plaats_{i}"
                });
            }

            var res = new ResponseModel
            {
                Message = "Hello World"
            };

            res.Add("AuthKey",Guid.NewGuid().ToString());
            res.Add("User",string.IsNullOrEmpty(User.Identity.Name) ? "None" : User.Identity.Name);
            res.AddList("Bedrijf",compannies);

            res.AddArrayIn("Bedrijf", "test", new string[] {"Hell",  "World"});

            return Json(res);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
