using BPVAPP_Backend.Database.Models;
namespace BPVAPP_Backend.Utils
{
    public static class Validation
    {

        public static bool IsValidCompany(CompanyModel model)
        {
            if (string.IsNullOrEmpty(model.Bedrijfsnaam)) return false;
            if (string.IsNullOrEmpty(model.Adres)) return false;
            if (string.IsNullOrEmpty(model.Website)) return false;
            if (string.IsNullOrEmpty(model.Plaats)) return false;
            if (string.IsNullOrEmpty(model.PostCode)) return false;
            if (string.IsNullOrEmpty(model.TelefoonNummer)) return false;
            if (string.IsNullOrEmpty(model.ContactPersoon_1)) return false;
            if (string.IsNullOrEmpty(model.ContactPersoonEmail_1)) return false;
            return true;
        }

        public static bool IsValidStudent(StudentModel model)
        {
            if (model.StudentNumber == 0) return false;
            if (string.IsNullOrEmpty(model.FirstName)) return false;
            if (string.IsNullOrEmpty(model.TussenVoegsel)) return false;
            if (string.IsNullOrEmpty(model.LastName)) return false;
            if (string.IsNullOrEmpty(model.Woonplaats)) return false;
            if (string.IsNullOrEmpty(model.Adres)) return false;
            return true;
        }
    }
}
