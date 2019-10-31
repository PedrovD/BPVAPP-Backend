using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BPVAPP_Backend.Database.Models
{
    public class StudentModel
    {
        public virtual int Id { get; set; }
        public virtual string StudentNumber { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string TussenVoegsel { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Woonplaats { get; set; }
        public virtual string Adres { get; set; }
        public virtual string Class { get; set; }
        public virtual string PhoneNumber { get; set; }
        public virtual string HasInternship { get; set; }
        public virtual DateTime StartDate { get; set; }
        public virtual DateTime EndDate { get; set; }
    }
}
