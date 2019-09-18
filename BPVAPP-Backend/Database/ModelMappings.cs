using BPVAPP_Backend.Database.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static BPVAPP_Backend.Database.Models.LeerlingModel;

namespace BPVAPP_Backend.Database
{
    // Place all the mappings of models in this.

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
            Map(i => i.CatagoryId);
            HasOne(i => i.Begeleider);
        }
    }


    public class ProductModelMapping : ClassMap<ProductModel>
    {
        public ProductModelMapping()
        {
            Table("StagebegeleiderModelTable");
            Id(i => i.Id).GeneratedBy.Identity();
            Map(i => i.FirstName);
            Map(i => i.LastName);
            Map(i => i.BegeleiderId);
            Map(i => i.Postcode);
            Map(i => i.Mail);
            Map(i => i.Telephone);
            Map(i => i.Password);
            Map(i => i.CatagoryId);
            HasOne(i => i.Begeleider);
        }
    }




}
