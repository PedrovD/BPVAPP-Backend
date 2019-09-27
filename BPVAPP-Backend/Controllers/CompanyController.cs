using System;
using Microsoft.AspNetCore.Mvc;
using BPVAPP_Backend.Database;
using BPVAPP_Backend.Database.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using BPVAPP_Backend.Response;
using System.Collections.Generic;

namespace BPVAPP_Backend.Controllers
{
    [Route("api/[controller]")]
    public class CompanyController : Controller
    {
        private readonly DbConnection dbConnection;

        public CompanyController()
        {
            dbConnection = new DbConnection();
        }

        [HttpGet]
        [Route("add")]
        public object CreateCompany(CompanyModel model)
        {
            dbConnection.AddModel(model);

            var rs = new ResponseModel
            {
                Message = $"Bedrijf '{model.Bedrijfsnaam}' is toegevoegd"
            };
            rs.Add("bedrijfId", model.Id);

            return Json(rs);
        }

        [HttpGet]
        [Route("delete/{id}")]
        public object DeleteCompanyById(int id)
        {
            var res = new ResponseModel();
            var model = dbConnection.GetCompanyById(id);

            if (model == null)
            {
                Response.StatusCode = 404;
                res.Message = $"Bedrijf is niet gevonden";
                return Json(res);
            }

            dbConnection.DeleteModel(model);
            res.Message = "Bedrijf is verwijderd";
            
            return Json(res);
        }

        [HttpGet]
        [Route("get/{id}")]
        public object GetCompanyById(int id)
        {
            var res = new ResponseModel();

            var model = dbConnection.GetCompanyById(id);

            if(model == null)
            {
                Response.StatusCode = 404;
                res.Message = $"Bedrijf is niet gevonden";
                return Json(res);
            }

            res.Message = $"Bedrijf gevonden";
            res.AddList("Bedrijf",new List<CompanyModel> { model });

            return Json(res);
        }
    }
}