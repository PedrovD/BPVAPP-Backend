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
            //Map(i => i.Adres);
            Map(i => i.Website).Default("0");
            //Map(i => i.Plaats);
            //Map(i => i.PostCode);
            Map(i => i.TelefoonNummer);
            //Map(i => i.ContactPersoon_1);
            //Map(i => i.ContactPersoonEmail_1);
            //Map(i => i.ContactPersoon_2);
            //Map(i => i.ContactPersoonEmail_2);
            Map(i => i.Opmerking);
        }
    }

    public class CompanyLocationMapping : ClassMap<CompanyLocations>
    {
        public CompanyLocationMapping()
        {
            Table("company_location_table");
            Id(i => i.Id);
            Map(i => i.Address).Length(255);
            Map(i => i.Plaats);
            Map(i => i.Postcode);
            Map(i => i.CompanyId);
        }
    }


    public class LeerlingModelMapping : ClassMap<LeerlingModel>
    {
        public LeerlingModelMapping()
        {
            Table("LeerlingModelTable");
            Id(i => i.Id).GeneratedBy.Identity();
            Map(i => i.FirstName);
            Map(i => i.LastName);
            Map(i => i.BegeleiderId);
            Map(i => i.Postcode);
            Map(i => i.Mail);
            Map(i => i.Telephone);
            Map(i => i.Password);
            //Map(i => i.CatagoryId);
            HasOne(i => i.Begeleider);
        }
    }


    public class ProductModelMapping : ClassMap<StagebegeleiderModel>
    {
        public ProductModelMapping()
        {
            Table("StagebegeleiderModelTable");
            Id(i => i.Id).GeneratedBy.Identity();
            Map(i => i.FirstName);
            Map(i => i.LastName);
            Map(i => i.LeerlingId);
            Map(i => i.Mail);
            Map(i => i.Telephone);
            Map(i => i.Password);
            Map(i => i.CatagoryId);
            HasMany(i => i.leerlingen);
        }
    }



}
