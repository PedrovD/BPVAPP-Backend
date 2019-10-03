using FluentNHibernate.Mapping;

namespace BPVAPP_Backend.Database.Models
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
            Map(i => i.ContactPersoon_2);
            Map(i => i.ContactPersoonEmail_2);
            Map(i => i.Opmerking);
        }
    }
    public class StudentMapping : ClassMap<StudentModel>
    {
        public StudentMapping()
        {
            Table("student_table");
            Id(i => i.Id);
            Map(i => i.FrontName);
            Map(i => i.LastName);
            Map(i => i.StudentNumber);
            Map(i => i.Class);
        }
    }
    public class ClassMapping : ClassMap<ClassModel>
    {
        public ClassMapping()
        {
            Table("class_table");
            Id(i => i.Id);
            Map(i => i.Class);
        }
    }
}