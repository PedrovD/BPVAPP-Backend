using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BPVAPP_Backend.Database.Models
{
    public class LeerlingToCompany
    {
        public virtual int Id { get; set; }
        public virtual int Leerling { get; set; }
        public virtual int CompanyContactId { get; set; }
        public virtual string StageStart { get; set; }
        public virtual string StageEnd { get; set; }
        public virtual int CompanyId { get; set; }
    }
}
