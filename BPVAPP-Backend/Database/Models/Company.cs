﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BPVAPP_Backend.Database.Models
{
    public class Company
    {
        public virtual int Id { get; set; }
        public virtual int ContactpersoonId { get; set; }
        public virtual string Bedrijfsnaam { get; set; }
        public virtual string Postcode { get; set; }
        public virtual string Plaats { get; set; }
        public virtual string Straat { get; set; }
        public virtual string Address { get; set; }
        public virtual string Website { get; set; }
        public virtual string TelefoonNummer { get; set; }
        public virtual string Opmerking { get; set; }
    }
}
