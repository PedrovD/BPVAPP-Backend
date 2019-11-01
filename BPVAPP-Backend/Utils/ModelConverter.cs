using BPVAPP_Backend.Database.Models;
using System.Collections.Generic;
using System.Linq;

namespace BPVAPP_Backend.Utils
{
    public static class ModelConverter
    {

        public static List<KeyStudentModel> ConvertToKeyStudent(StudentModel[] students)
        {
            var list = new List<KeyStudentModel>();

            for (var i = 0; i < students.Length; i++)
            {
                var std = students[i];
                list.Add(new KeyStudentModel {
                 Key = i,
                 Adres = std.Adres,
                 Class = std.Class,
                 EndDate = std.EndDate,
                 FirstName = std.FirstName,
                 HasInternship = std.HasInternship,
                 Id = std.Id,
                 LastName = std.LastName,
                 PhoneNumber = std.PhoneNumber,
                 StartDate = std.StartDate,
                 StudentNumber = std.StudentNumber,
                 TussenVoegsel = std.TussenVoegsel,
                 Woonplaats = std.Woonplaats,
                });
            }

            return list;
        }

    }
}
