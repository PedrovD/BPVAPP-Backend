using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BPVAPP_Backend.Database.Models
{
    public class KeyStudentModel
    {
        public  int Id { get; set; }
        public  string StudentNumber { get; set; }
        public  string FirstName { get; set; }
        public  string TussenVoegsel { get; set; }
        public  string LastName { get; set; }
        public  string Woonplaats { get; set; }
        public  string Adres { get; set; }
        public  string Class { get; set; }
        public  string PhoneNumber { get; set; }
        public string HasInternship { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Key { get; set; }
    }
}
