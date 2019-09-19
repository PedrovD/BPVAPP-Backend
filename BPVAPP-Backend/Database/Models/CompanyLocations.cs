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
        public virtual string Locatie { get; set; }
        public virtual int BedrijfId { get; set; }
    }
}
