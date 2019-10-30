﻿namespace BPVAPP_Backend.Database.Models
{
    public class CompanyModel
    {
        public virtual int Id { get; set; }
        public virtual string Bedrijfsnaam { get; set; }
        public virtual string Adres { get; set; }
        public virtual string Website { get; set; }
        public virtual string Plaats { get; set; }
        public virtual string PostCode { get; set; }
        public virtual string TelefoonNummer { get; set; }
        public virtual int Capacity { get; set; }
        public virtual int CurrentInterns { get; set; }
        public virtual string StdNumbers { get; set; }
        public virtual string ContactPersoon_1 { get; set; }
        public virtual string ContactPersoonEmail_1 { get; set; }
        public virtual string ContactPersoon_2 { get; set; }
        public virtual string ContactPersoonEmail_2 { get; set; }
        public virtual string Opmerking { get; set; }
        public virtual string Languages { get; set; }
        public virtual string FrameWorks { get; set; }
        public virtual string Longitude { get; set; }
        public virtual string Latitude { get; set; }
    }
}