using HumanResources.Entity.Entities.Common;
using HumanResources.Entity.Enums;
using System.Collections.Generic;

namespace HumanResources.Entity.Entities
{
    public class Demirbas : BaseEntity
    {
        // Hangi kategori? (Laptop, Monitör, Telefon...)
        public int ZimmetTuruId { get; set; }
        public ZimmetTuru ZimmetTuru { get; set; }

        public string Marka { get; set; }
        public string Model { get; set; }
        public string? SeriNumarasi { get; set; }   // Her demirbaþýn seri no'su olmayabilir
        public string? Aciklama { get; set; }

        public DemirbasDurumu Durumu { get; set; }

        // Bu demirbaþýn geçmiþ + aktif tüm zimmet kayýtlarý
        public ICollection<Zimmet> Zimmetler { get; set; }
    }
}