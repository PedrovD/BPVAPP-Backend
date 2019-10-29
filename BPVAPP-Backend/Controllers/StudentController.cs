using System;
using Microsoft.AspNetCore.Mvc;
using BPVAPP_Backend.Database;
using BPVAPP_Backend.Database.Models;
using static BPVAPP_Backend.Utils.Validation;
using BPVAPP_Backend.Response;
using System.Collections.Generic;

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

            var result = dbConnection.SearchStudent(query);

            if (result == null || result.Count == 0)
            {
                rs.Message = "Geen resultaat";
                return Json(rs);
            }

            rs.Message = $"{result.Count} Gevonden";
            rs.AddList("Student", result);

            return Json(rs);
        }

        [HttpGet]
        [Route("get/class/{Class}")]
        public object GetStudentsByClass(string Class)
        {
            var rs = new ResponseModel();

            var result = dbConnection.GetStudentsByClass(Class);

            if (result == null || result.Count == 0)
            {
                rs.Message = "Geen resultaat";
                return Json(rs);
            }

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

            res.Message = $"Student gevonden";
            res.AddList("Student", new List<StudentModel> {  model });

            return Json(res);
        }
        [HttpGet]
        [Route("all")]
        public object GetStudents()
        {
            var res = new ResponseModel();

            var models = dbConnection.GetAllModels<StudentModel>();

            if (models == null || models.Count == 0)
            {
                Response.StatusCode = 404;
                res.Message = $"Studenten zijn niet gevonden";
                return Json(res);
            }


            res.Message = $"Studenten gevonden";
            res.AddList("Studenten", models);

            return Json(res);
        }

    }
}
