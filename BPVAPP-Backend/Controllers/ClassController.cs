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
    public class ClassController : Controller
    {
        private readonly DbConnection dbConnection;

        public ClassController()
        {
            dbConnection = new DbConnection();
        }
        [HttpPost]
        [Route("add")]
        public object CreateClass(ClassModel model)
        {
            if (string.IsNullOrEmpty(model.Class))
            {
                return Json(new ResponseModel {
                    Message = "Velden niet ingevuld"
                });
            }

            dbConnection.AddModel(model);
            var rs = new ResponseModel
            {
                Message = $"Student '{model.Class}' is toegevoegd"
            };
            rs.Add("StudentId", model.Id);

            return Json(rs);
        }
        [HttpGet]
        [Route("get/{id}")]
        public object GetClassById(int id)
        {
            var res = new ResponseModel();

            var model = dbConnection.GetClassById(id);

            if (model == null)
            {
                Response.StatusCode = 404;
                res.Message = $"Klas is niet gevonden";
                return Json(res);
            }

            res.Message = $"Klas gevonden";
            res.AddList("Klas", new List<ClassModel> { });

            return Json(res);
        }
        [HttpGet]
        [Route("all")]
        public object GetClasses()
        {
            var res = new ResponseModel();

            var models = dbConnection.GetAllModels<ClassModel>();

            if (models == null || models.Count == 0)
            {
                Response.StatusCode = 404;
                res.Message = $"Klassen zijn niet gevonden";
                return Json(res);
            }
            

            res.Message = $"Klassen gevonden";
            res.AddList("Klassen",  models );

            return Json(res);
        }
    }
}
