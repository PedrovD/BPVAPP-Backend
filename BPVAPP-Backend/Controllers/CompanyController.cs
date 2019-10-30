using System;
using Microsoft.AspNetCore.Mvc;
using BPVAPP_Backend.Database;
using BPVAPP_Backend.Database.Models;
using static BPVAPP_Backend.Utils.Validation;
using BPVAPP_Backend.Response;
using System.Collections.Generic;
using BPVAPP_Backend.Utils;

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
        [Route("all")]
        public object GetAll()
        {
            var res = new ResponseModel();
            var companies = dbConnection.GetAllModels<CompanyModel>();

            if (companies == null)
            {
                Response.StatusCode = 404;
                res.Message = "Geen bedrijven";
                return Json(res);
            }

            res.Message = $"{companies.Count} Bedrijven";
            res.AddList("Bedrijf", companies);

            return Json(res);
        }

       /* [HttpGet]
        [Route("updategeo")]
        public object UpdateGeolocation()
        {
            var rs = new ResponseModel();

            var companies = dbConnection.GetAllModels<CompanyModel>();

            if (companies == null)
            {
                Response.StatusCode = 404;
                rs.StatusCode = 404;
                rs.Message = "Geen bedrijven";
                return Json(rs);
            }

            foreach (var company in companies)
            {
                var loc = MapQuestHelper.GetGeoLocation(company.Adres);

                if (loc == null) continue;

                company.Longitude = loc.Longitude;
                company.Latitude = loc.Latitude;

                dbConnection.SaveOrUpdateModel(company);

            }

            rs.Message = "Geslaagd!";

            return Json(rs);
        }*/

        [HttpGet]
        [Route("mapdata")]
        public object MapLocations()
        {
            var rs = new ResponseModel();

            var companies = dbConnection.GetAllModels<CompanyModel>();

            if (companies == null)
            {
                Response.StatusCode = 404;
                rs.StatusCode = 404;
                rs.Message = "Geen bedrijven";
                return Json(rs);
            }

            rs.AddList("geolocations", FetchGeoLocations(companies));

            return Json(rs);
        }

        [HttpGet]
        [Route("search/{query}")]
        public object Search(string query)
        {
            var rs = new ResponseModel();   

            var result = dbConnection.SearchCompany(query);

            if (result == null || result.Count == 0)
            {
                rs.Message = "Geen resultaat";
                rs.StatusCode = 404;
                return Json(rs);
            }

            rs.Message = $"{result.Count} Gevonden";
            rs.AddList("Bedrijf", result);

            return Json(rs);
        }

        [HttpPost]
        [Route("add")]
        public object CreateCompany([FromBody]CompanyModel model)
        {
            dbConnection.SaveOrUpdateModel(model);

            var rs = new ResponseModel
            {
                Message = $"Bedrijf '{model.Bedrijfsnaam}' is toegevoegd"
            };
            rs.Add("bedrijfId", model.Id);

            return Json(rs);
        }

        [HttpPost]
        [Route("save")]
        public object SaveCompany([FromBody]CompanyModel model)
        {
            var rs = new ResponseModel();

            var company = dbConnection.GetCompanyById(model.Id);

            if (company == null)
            {
                Response.StatusCode = 404;
                rs.StatusCode = 404;
                rs.Message = "Bedrijf niet gevonden";
                return Json(rs);
            }
            dbConnection.SaveOrUpdateModel(model);
            rs.Message = "Opgeslagen!";
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
                res.StatusCode = 404;
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
                res.StatusCode = 404;
                res.Message = $"Bedrijf is niet gevonden";
                return Json(res);
            }

            res.Message = $"Bedrijf gevonden";
            res.AddList("Bedrijf",new List<CompanyModel> { model });

            return Json(res);
        }

        [Route("add/{id}")]
        public object AddStudentToCompany(int id, [FromBody]StudentModel Student)
        {
            var res = new ResponseModel();

            var model = dbConnection.GetCompanyById(id);

            var student = dbConnection.GetStudentByStdNumber(Student.StudentNumber);

            model.CurrentInterns++;
            model.StdNumbers += $"{student.StudentNumber},";
            if (model == null)
            {
                Response.StatusCode = 404;
                res.StatusCode = 404;
                res.Message = $"Bedrijf is niet gevonden";
                return Json(res);
            }

            dbConnection.SaveOrUpdateModel(student);
            dbConnection.SaveOrUpdateModel(model);
            res.Message = $"Student toegevoegd aan bedrijf!";

            return Json(res);
        }


        [NonAction]
        private List<string> FetchAddresList(List<CompanyModel> companies)
        {
            var list = new List<string>();
            foreach (var company in companies)
            {
                if (string.IsNullOrEmpty(company.Plaats) || string.IsNullOrEmpty(company.Adres)) continue;

                list.Add($"{company.Adres.Trim()} {company.Plaats.Trim()}");
            }
            return list;
        }

        [NonAction]
        private List<GeoLocation> FetchGeoLocations(List<CompanyModel> companies)
        {
            var list = new List<GeoLocation>();

            foreach (var comp in companies)
            {
                if (string.IsNullOrEmpty(comp.Latitude) || string.IsNullOrEmpty(comp.Longitude)) continue;

                list.Add(new GeoLocation {
                    Latitude = comp.Latitude,
                    Longitude = comp.Longitude
                });
            }

            return list;
        }

        [NonAction]
        private List<string> FetchStatusList(List<CompanyModel> companies)
        {
            var list = new List<string>();
            foreach (var company in companies)
            {
                list.Add("Geen");
            }
            return list;
        }
    }
}