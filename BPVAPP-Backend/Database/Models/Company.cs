using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BPVAPP_Backend.Database.Models
{
    public class Company
    {
        public virtual int Id { get; set; }
        public virtual string Bedrijfsnaam { get; set; }
        public virtual string Adres { get; set; }
        public virtual string Website { get; set; }
        public virtual string Plaats { get; set; }
        public virtual string PostCode { get; set; }
        public virtual string TelefoonNummer { get; set; }
        public virtual string Opmerking { get; set; }
    }
}
