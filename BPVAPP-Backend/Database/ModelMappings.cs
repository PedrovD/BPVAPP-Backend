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
            Map(i => i.Address);
            Map(i => i.Website).Default("0");
            Map(i => i.Plaats);
            Map(i => i.Postcode);
            Map(i => i.TelefoonNummer);
            Map(i => i.ContactpersoonId);
            Map(i => i.Straat);
            Map(i => i.Opmerking);
            //Map(i => i.ContactPersoon_1);
            //Map(i => i.ContactPersoonEmail_1);
            //Map(i => i.ContactPersoon_2);
            //Map(i => i.ContactPersoonEmail_2);

        }
    }

    public class LeerlingToCompanyLocationMapping : ClassMap<LeerlingToCompany>
    {
        public LeerlingToCompanyLocationMapping()
        {
            Table("company_location_table");
            Id(i => i.Id);
            Map(i => i.CompanyContactId).Length(255);
            Map(i => i.CompanyId);
            Map(i => i.Leerling);
            Map(i => i.StageEnd);
            Map(i => i.StageStart);
        }
    }


    public class LeerlingModelMapping : ClassMap<LeerlingModel>
    {
        public LeerlingModelMapping()
        {
            Table("LeerlingModelTable");
            Id(i => i.Id).GeneratedBy.Identity();
            Map(i => i.achternaam);
            Map(i => i.adres);
            Map(i => i.Mail);
            Map(i => i.Password);
            Map(i => i.Postcode);
            Map(i => i.telefoonNr);
            Map(i => i.tussenvoegsel);
            Map(i => i.voornaam);
            Map(i => i.woonplaats);
            //Map(i => i.CatagoryId);
        }
    }


    public class CompanyContactModelMapping : ClassMap<CompanyContact>
    {
        public CompanyContactModelMapping()
        {
            Table("StagebegeleiderModelTable");
            Id(i => i.Id).GeneratedBy.Identity();
            Map(i => i.FirstName);
            Map(i => i.LastName);
            Map(i => i.BedrijfsId);
            Map(i => i.BedrijfsId);
            Map(i => i.Mail);
            Map(i => i.Telephone);
            Map(i => i.Password);
            //Map(i => i.CatagoryId);
            //HasMany(i => i.leerlingen);
        }
    }



}
