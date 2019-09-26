using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using BPVAPP_Backend.Database.Models;
using Newtonsoft.Json;

namespace BPVAPP_Backend.Utils
{
    public class JsonResponse
    {
        private readonly JObject _base;

        public JsonResponse(int status_code = 200, string message = "success")
        {
            _base = new JObject {
                ["time"] = DateTime.Now.ToString(),
                ["status"] = status_code.ToString(),
                ["message"] = message,
                ["data"] = new JArray() {
                    [0] = new JObject()
                },
                
            };
        }

        public void AddCompany(CompanyModel model)
        {
            var js = JsonConvert.SerializeObject(model);

            var c = (JObject)_base["data"][0];
            c.Add(js);
        }

        public void AddData(string name, object item)
        {
            var data = _base["data"];

            data[name] = item.ToString();
        }

        public override string ToString()
        {
            return _base.ToString();
        }
    }
}
