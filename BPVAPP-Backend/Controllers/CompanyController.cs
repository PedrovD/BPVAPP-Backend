using System;
using Microsoft.AspNetCore.Mvc;
using BPVAPP_Backend.Database;
using BPVAPP_Backend.Database.Models;
using BPVAPP_Backend.Response;
using System.Collections.Generic;
using BPVAPP_Backend.Utils;
using static BPVAPP_Backend.Utils.ModelConverter;

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

        /*[HttpGet] // If you ever uncomment and call this function, i will hunt you down and kill you :)
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
            // This will take like 2-3 min to complete
            foreach (var company in companies)
            {
                if (string.IsNullOrEmpty(company.Adres) || string.IsNullOrEmpty(company.Plaats)) continue;

                var loc = MapQuestHelper.GetGeoLocationGoogle($"{company.Adres.Trim()} {company.Plaats.Trim()}");

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
            if (!string.IsNullOrEmpty(model.Adres) && !string.IsNullOrEmpty(model.Plaats))
            {
                var loc = MapQuestHelper.GetGeoLocationGoogle($"{model.Adres.Trim()} {model.Plaats.Trim()}");

                if (loc == null)
                {
                    model.Latitude = "null";
                    model.Longitude = "null";
                }
                else
                {
                    model.Latitude = loc.Latitude;
                    model.Longitude = loc.Longitude;
                }
            }

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

            if (!string.IsNullOrEmpty(model.Adres) && !string.IsNullOrEmpty(model.Plaats))
            {
                var loc = MapQuestHelper.GetGeoLocationGoogle($"{model.Adres.Trim()} {model.Plaats.Trim()}");

                if (loc == null)
                {
                    model.Adres = company.Adres;
                    model.Plaats = company.Plaats;
                }
            }

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
            var company = dbConnection.GetCompanyById(id);

            if (company == null)
            {
                Response.StatusCode = 404;
                res.StatusCode = 404;
                res.Message = $"Bedrijf is niet gevonden";
                return Json(res);
            }
            var studentsList = new List<StudentModel>();
            res.Message = $"Bedrijf gevonden";
            res.AddList("Bedrijf",new List<CompanyModel> { company });

            if (!string.IsNullOrEmpty(company.StdNumbers))
            {
                var students = company.StdNumbers.Split(",");
                for (int i = 0; i < students.Length; i++)
                {
                    if (!string.IsNullOrEmpty(students[i]))
                    {
                        var student = dbConnection.GetStudentByStudentNumber(students[i]);
                        studentsList.Add(student);
                    }
                }
                res.AddList("Studenten", ConvertToKeyStudent(studentsList.ToArray()));
            }

            return Json(res);
        }
        [HttpPost]
        [Route("removeintern/{id}")]
        public object RemoveStudentFromCompany(int id, [FromBody]StudentModel studentnumber)
        {
            var res = new ResponseModel();
            var newString = "";
            var company = dbConnection.GetCompanyById(id);
            var student = dbConnection.GetStudentByStudentNumber(studentnumber.StudentNumber);
            var students = company.StdNumbers.Split(",");
            for (int i = 0; i < students.Length; i++)
            {
                if (students[i] == student.StudentNumber)
                {
                    student.HasInternship = "Nee";
                    company.CurrentInterns--;
                    students[i] = "";
                    break;
                }
            }
            for (int i = 0; i < students.Length; i++)
            {
                if (!string.IsNullOrEmpty(students[i]))
                {
                    if(i == 0)
                    {
                        newString += students[i];
                    }
                    else
                    {
                        newString += ","+students[i];
                    }
                }
            }
            company.StdNumbers = newString;

            dbConnection.SaveOrUpdateModel(company);
            dbConnection.SaveOrUpdateModel(student);

            return Json(res);
        }

        [HttpPost]
        [Route("add/{id}")]
        public object AddStudentToCompany(int id, [FromBody]AddIntersModel inters)
        {
            var res = new ResponseModel();

            var company = dbConnection.GetCompanyById(id);
            if (company == null)
            {
                Response.StatusCode = 404;
                res.StatusCode = 404;
                res.Message = $"Bedrijf is niet gevonden";
                return Json(res);
            }

            if (company.CurrentInterns >= company.Capacity)
            {
                res.Message = "Bedrijf zit vol!";
                return Json(res);
            }

            var studentlist = new List<StudentModel>();

            for (var i = 0; i < inters.Students.Length; i++)
            {
                var std = dbConnection.GetStudentByStudentNumber(inters.Students[i]);

                if (std == null) continue;
                if (company.StdNumbers.Contains(inters.Students[i])) continue;
                if (string.IsNullOrEmpty(inters.StartDates[i]) || string.IsNullOrEmpty(inters.EndDates[i])) continue;

                DateTime.TryParse(inters.StartDates[i], out var start);
                DateTime.TryParse(inters.EndDates[i], out var end);

                std.StartDate = start;
                std.EndDate = end;

                studentlist.Add(std);
            }

            if (studentlist.Count == 0)
            {
                Response.StatusCode = 404;
                res.StatusCode = 404;
                res.Message = $"{(inters.Students.Length > 1 ? "Studenten zijn niet gevonden of lopen al stage bij dit bedrijf" : "Student is niet gevonden of loopt al stage bij dit bedrijf")}";
                return Json(res);
            }

            //if(string.IsNullOrEmpty(company.StdNumbers))
            //{
            //    company.StdNumbers = string.Empty;
            //}

            for (var i = 0; i < studentlist.Count; i++)
            {
                if (!(company.CurrentInterns >= company.Capacity))
                {
                    company.CurrentInterns++;
                    company.StdNumbers += $"{(company.StdNumbers.Length > 1 ? $",{studentlist[i].StudentNumber}" : $"{studentlist[i].StudentNumber}")}";
                    studentlist[i].HasInternship = "Ja";

                    //dbConnection.SaveOrUpdateModel(company);
                    dbConnection.SaveOrUpdateModel(studentlist[i]);
                }
            }
            dbConnection.SaveOrUpdateModel(company);

            res.Message = $"{(studentlist.Count > 1 ? "Studenten zijn gekoppeld aan bedrijf" : "Student is gekoppeld aan bedrijf")}";

            return Json(res);
        }

        [NonAction]
        private List<GeoLocation> FetchGeoLocations(List<CompanyModel> companies)
        {
            var list = new List<GeoLocation>();

            foreach (var comp in companies)
            {
                if (string.IsNullOrEmpty(comp.Latitude) || 
                    string.IsNullOrEmpty(comp.Longitude)||
                    comp.Latitude.Equals("null") ||
                    comp.Longitude.Equals("null") ||
                    string.IsNullOrEmpty(comp.Bedrijfsnaam) ||
                    string.IsNullOrEmpty($"{comp.Adres.Trim()} {comp.Plaats.Trim()} {comp.PostCode.Trim()}")) continue;

                list.Add(new GeoLocation {
                    CompanyName = comp.Bedrijfsnaam,
                    Latitude = comp.Latitude.Replace(",","."),
                    Longitude = comp.Longitude.Replace(",", "."),
                    CompanyAdres = $"{comp.Adres.Trim()} {comp.Plaats.Trim()} {comp.PostCode.Trim()}"
                });
            }

            return list;
        }
    }
}