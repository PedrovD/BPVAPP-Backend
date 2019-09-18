using BPVAPP_Backend.Database.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static BPVAPP_Backend.Database.Models.LeerlingModel;

namespace BPVAPP_Backend.Database
{

    public class CompanyMapping : ClassMap<Company>
    {
        public CompanyMapping()
        {
            Table("company_table");
            Id(i => i.Id);
            Map(i => i.Bedrijfsnaam).Length(255);
            Map(i => i.Adres);
            Map(i => i.Website).Default("0");
            Map(i => i.Plaats);
            Map(i => i.PostCode);
            Map(i => i.TelefoonNummer);
            Map(i => i.ContactPersoon_1);
            Map(i => i.ContactPersoonEmail_1);
            Map(i => i.ContactPersoon_2);
            Map(i => i.ContactPersoonEmail_2);
            Map(i => i.Opmerking);
        }
    }
}
