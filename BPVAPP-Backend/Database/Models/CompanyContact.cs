using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BPVAPP_Backend.Database.Models
{
    public class CompanyContact
    {
        public virtual int Id { get; set; }
        public virtual int BedrijfsId { get; set; }
        public virtual int LocatieId { get; set; }
        public virtual string Naam { get; set; }
        public virtual string Email { get; set; }
        public virtual int TelefoonNummer { get; set; }
        public virtual string Opmerking { get; set; }
    }
}
