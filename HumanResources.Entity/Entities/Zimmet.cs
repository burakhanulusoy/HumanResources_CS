using HumanResources.Entity.Entities.Common;
using HumanResources.Entity.Enums;

namespace HumanResources.Entity.Entities
{
    public class Zimmet : BaseEntity
    {
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        // Marka/Model/Seri artýk burada YOK -> hazýr demirbaþ seçiliyor
        public int DemirbasId { get; set; }
        public Demirbas Demirbas { get; set; }

        public DateTime TeslimTarihi { get; set; }

        // Süresiz zimmette iade tarihi olmaz -> nullable
        public DateTime? IadeTarihi { get; set; }
        public bool SuresizMi { get; set; }

        public ZimmetDurumu Durumu { get; set; }
        public string? Aciklama { get; set; }
    }
}