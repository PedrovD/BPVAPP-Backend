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
    public class StudentController : Controller
    {
        private readonly DbConnection dbConnection;

        public StudentController()
        {
            dbConnection = new DbConnection();
        }

        [HttpPost]
        [Route("add")]
        public object CreateStudent(StudentModel model)
        {
            dbConnection.AddModel(model);

            var rs = new ResponseModel
            {
                Message = $"Student '{model.FrontName}' is toegevoegd"
            };
            rs.Add("StudentId", model.Id);

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
