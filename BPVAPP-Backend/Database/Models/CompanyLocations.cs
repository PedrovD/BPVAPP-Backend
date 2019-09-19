using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BPVAPP_Backend.Database.Models
{
    public class CompanyLocations
    {
        public virtual int Id { get; set; }

        public virtual string Postcode { get; set; }
        public virtual string Plaats { get; set; }
        public virtual string Staats { get; set; }
        public virtual string Address { get; set; }
        public virtual int CompanyId { get; set; }
    }
}
