using HumanResources.Entity.Entities.Common;
using HumanResources.Entity.Enums;

namespace HumanResources.Entity.Entities
{
    public class Zimmet : BaseEntity
    {
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public int ZimmetTuruId { get; set; }
        public ZimmetTuru ZimmetTuru { get; set; }
        public string? SeriNumarasi { get; set; }

        // YENÝ
        public string? Marka { get; set; }
        public string? Model { get; set; }

        public DateTime TeslimTarihi { get; set; }
        public DateTime IadeTarihi { get; set; }
        public ZimmetDurumu Durumu { get; set; }
        public string Aciklama { get; set; }
    }
}