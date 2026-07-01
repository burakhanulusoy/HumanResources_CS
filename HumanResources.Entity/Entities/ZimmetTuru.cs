using HumanResources.Entity.Entities.Common;
using System.Collections.Generic;

namespace HumanResources.Entity.Entities
{
    public class ZimmetTuru : BaseEntity
    {
        public string Ad { get; set; } // Örn: Laptop, Monitör, ESD Bileklik, Ýþ Ayakkabýsý

        public IList<Zimmet> Zimmetler { get; set; }
    }
}