using HumanResources.Entity.Entities.Common;
using System.Collections.Generic;

namespace HumanResources.Entity.Entities
{
    public class ZimmetTuru : BaseEntity
    {
        public string Ad { get; set; } // Örn: Laptop, Monitör, ESD Bileklik
        // Artýk türe baðlý olan þey Zimmet deðil, fiziksel demirbaþlar
        public IList<Demirbas> Demirbaslar { get; set; }
    }
}