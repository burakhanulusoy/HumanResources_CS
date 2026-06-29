using HumanResources.Entity.Entities.Common;

namespace HumanResources.Entity.Entities
{
    public class Departman : BaseEntity
    {
        public string Ad { get; set; }

        // Departman Yöneticisi Ýliþkisi (int olarak güncellendi)
        public int? YoneticiId { get; set; }
        public AppUser? Yonetici { get; set; }

        public IList<Birim> Birimler { get; set; }
        public IList<AppUser> Personeller { get; set; }
    }
}
