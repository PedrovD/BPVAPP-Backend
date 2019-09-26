using BPVAPP_Backend.Database.Models;
using FluentNHibernate.Mapping;

namespace BPVAPP_Backend.Database
{

    public class CompanyMapping : ClassMap<CompanyModel>
    {
        public CompanyMapping()
        {
            Table("company_table");
            Id(i => i.Id);
            Map(i => i.Bedrijfsnaam);
            Map(i => i.Adres);
            Map(i => i.Website);
            Map(i => i.Plaats);
            Map(i => i.PostCode);
            Map(i => i.TelefoonNummer);
            Map(i => i.ContactPersoon_1);
            Map(i => i.ContactPersoonEmail_1);
            Map(i => i.ContactPersoon_2).Default("N.v.t.");
            Map(i => i.ContactPersoonEmail_2).Default("N.v.t.");
            Map(i => i.Opmerking).Default("N.v.t.");
        }
    }
}