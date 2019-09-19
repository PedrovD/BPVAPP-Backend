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
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual IList<StagebegeleiderModel> leerlingen { get; set; }
        public virtual string Mail { get; set; }
        public virtual string Telephone { get; set; }
        public virtual string Password { get; set; }

    }
}
