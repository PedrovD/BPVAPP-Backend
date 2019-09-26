using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BPVAPP_Backend.Utils;
using BPVAPP_Backend.Database.Models;

namespace BPVAPP_Backend.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        public string Get()
        {
            var json = new JsonResponse();

            //for (var i = 0; i < 5; i++)
            //{
            //    json.AddData($"num_{i}", i);
            //}

            var company = new CompanyModel {
                Bedrijfsnaam = "jemoeder"
            };

            json.AddCompany(company);


            return json.ToString();
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
