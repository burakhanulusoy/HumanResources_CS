using HumanResources.Entity.Entities.Common;
using System.Collections.Generic;

namespace HumanResources.Entity.Entities
{
    public class SertifikaTuru : BaseEntity
    {
        public string Ad { get; set; } 
        public string? Aciklama { get; set; } 

        public IList<Sertifika> Sertifikalar { get; set; }
    }
}