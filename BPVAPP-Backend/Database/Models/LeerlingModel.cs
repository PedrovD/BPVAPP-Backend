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
        public virtual string voornaam { get; set; }
        public virtual string tussenvoegsel { get; set; }
        public virtual string achternaam { get; set; }
        public virtual string woonplaats { get; set; }
        public virtual string adres { get; set; }
        public virtual string telefoonNr { get; set; }
        public virtual string Postcode { get; set; }
        public virtual string Mail { get; set; }
        public virtual string Password { get; set; }
    }
        // Can be removed later when we are using real models
    
}
