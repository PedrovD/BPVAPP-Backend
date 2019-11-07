using System;
using Microsoft.AspNetCore.Mvc;
using BPVAPP_Backend.Database;
using BPVAPP_Backend.Database.Models;
using BPVAPP_Backend.Response;
using System.Collections.Generic;
using System.Linq;

namespace BPVAPP_Backend.Controllers
{
    [Route("api/[controller]")]
    public class StudentController : Controller
    {
        private readonly DbConnection dbConnection;

        public StudentController()
        {
            dbConnection = new DbConnection();
        }

        [HttpPost]
        [Route("add")]
        public object CreateStudent([FromBody]StudentModel model)
        {
            model.StartDate = DateTime.Parse(model.StartDate.ToString("yyyy-MM-dd HH:mm:ss"));
            model.EndDate = DateTime.Parse(model.EndDate.ToString("yyyy-MM-dd HH:mm:ss"));

            dbConnection.SaveOrUpdateModel(model);

            var rs = new ResponseModel
            {
                Message = $"Student '{model.FirstName}' is toegevoegd"
            };
            rs.Add("StudentId", model.Id);
 
            return Json(rs);
        }

        [HttpPost]
        [Route("save")]
        public object SaveStudent([FromBody]StudentModel model)
        {
            var rs = new ResponseModel();

            var student = dbConnection.GetStudentById(model.Id);

            if (student == null)
            {
                Response.StatusCode = 404;
                rs.Message = "Student niet gevonden";
                return Json(rs);
            }

            model.StartDate = DateTime.Parse(model.StartDate.ToString("yyyy-MM-dd HH:mm:ss"));
            model.EndDate = DateTime.Parse(model.EndDate.ToString("yyyy-MM-dd HH:mm:ss"));

            dbConnection.SaveOrUpdateModel(model);

            rs.Message = "Opgeslagen!";
            return Json(rs);
        }

        [HttpGet]
        [Route("delete/{id}")]
        public object DeleteStudentById(int id)
        {
            var res = new ResponseModel();
            var model = dbConnection.GetStudentById(id);

            if (model == null)
            {
                Response.StatusCode = 404;
                res.StatusCode = 404;
                res.Message = $"Leerling is niet gevonden";
                return Json(res);
            }

            dbConnection.DeleteModel(model);
            res.Message = "Leerling is verwijderd";

            return Json(res);
        }

        [HttpGet]
        [Route("search/{query}")]
        public object Search(string query)
        {
            var rs = new ResponseModel();

            var result = dbConnection.SearchStudent(query).ToList(); ;

            if (result == null || result.Count == 0)
            {
                rs.Message = "Geen resultaat";
                return Json(rs);
            }

            result.ForEach(i => {
                i.StartDate = DateTime.Parse(i.StartDate.ToString("yyyy-MM-dd HH:mm:ss"));
                i.EndDate = DateTime.Parse(i.EndDate.ToString("yyyy-MM-dd HH:mm:ss"));
            });

            rs.Message = $"{result.Count} Gevonden";
            rs.AddList("Student", result);

            return Json(rs);
        }

        [HttpGet]
        [Route("get/class/{Class}")]
        public object GetStudentsByClass(string Class)
        {
            var rs = new ResponseModel();

            var result = dbConnection.GetStudentsByClass(Class).ToList();

            if (result == null || result.Count == 0)
            {
                rs.Message = "Geen resultaat";
                return Json(rs);
            }

            result.ForEach(i => {
                i.StartDate = DateTime.Parse(i.StartDate.ToString("yyyy-MM-dd HH:mm:ss"));
                i.EndDate = DateTime.Parse(i.EndDate.ToString("yyyy-MM-dd HH:mm:ss"));
            });

            rs.Message = $"{result.Count} Gevonden";
            rs.AddList("Student", result);

            return Json(rs);
        }

        [HttpGet]
        [Route("get/{id}")]
        public object GetStudentById(int id)
        {
            var res = new ResponseModel();

            var model = dbConnection.GetStudentById(id);

            if (model == null)
            {
                Response.StatusCode = 404;
                res.Message = $"Student is niet gevonden";
                return Json(res);
            }

            model.StartDate = DateTime.Parse(model.StartDate.ToString("yyyy-MM-dd HH:mm:ss"));
            model.EndDate = DateTime.Parse(model.EndDate.ToString("yyyy-MM-dd HH:mm:ss"));

            res.Message = $"Student gevonden";
            res.AddList("Student", new List<StudentModel> {  model });

            return Json(res);
        }
        [HttpGet]
        [Route("all")]
        public object GetStudents()
        {
            var res = new ResponseModel();

            var models = dbConnection.GetAllModels<StudentModel>().ToList();

            if (models == null || models.Count == 0)
            {
                Response.StatusCode = 404;
                res.Message = $"Studenten zijn niet gevonden";
                return Json(res);
            }

            models.ForEach(i => {
                i.StartDate = DateTime.Parse(i.StartDate.ToString("yyyy-MM-dd HH:mm:ss"));
                i.EndDate = DateTime.Parse(i.EndDate.ToString("yyyy-MM-dd HH:mm:ss"));
            });

            res.Message = $"Studenten gevonden";
            res.AddList("Studenten", models);

            return Json(res);
        }

    }
}
