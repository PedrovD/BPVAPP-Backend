using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BPVAPP_Backend.Database.Models
{
    public class StudentModel
    {
        public virtual int Id { get; set; }
        public virtual int StudentNumber { get; set; }
        public virtual string FrontName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Class { get; set; }
    }
}
