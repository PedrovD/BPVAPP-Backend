using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BPVAPP_Backend.Database.Models
{
    public class StagebegeleiderModel
    {
        public virtual int Id { get; protected set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual int CatagoryId { get; set; }
        public virtual string Mail { get; set; }
        public virtual string Telephone { get; set; }
        public virtual string Password { get; set; }

    }
}
