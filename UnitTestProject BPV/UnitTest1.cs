using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject_BPV
{

    class StudentModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SureName { get; set; }
        public string ClassId { get; set; }
    }

    [TestClass]
    public class UnitTest1 : Controller
    {

        List<StudentModel> Students = new List<StudentModel> {
            new StudentModel{ Id = 0, ClassId = "TIA4V3a", Name = "Davor", SureName = "Davor" },
            new StudentModel{ Id = 1, ClassId = "TIA4V3a", Name = "Mike", SureName = "van der leest" }
        };

        [TestMethod]
        public void GetStudents()
        {
            var result = Json(Students);
        }
    }
}
