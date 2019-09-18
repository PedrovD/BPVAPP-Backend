using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BPVAPP_Backend.Database.Models
{
        // This model is ussed for now as a refference for fluentnhibernate,
        public class LeerlingModel
        {
            public virtual int Id { get; protected set; }
            public virtual string FirstName { get; set; }
            public virtual string LastName { get; set; }
            public virtual int CatagoryId { get; set; }
            public virtual int BegeleiderId { get; set; }
            public virtual string Postcode { get; set; }
            public virtual string Mail { get; set; }
            public virtual string Telephone { get; set; }
            public virtual string Password { get; set; }
        public object Begeleider { get; internal set; }
    }
        // Can be removed later when we are using real models
    
}
