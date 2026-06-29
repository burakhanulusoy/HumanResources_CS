using HumanResources.Entity.Entities.Common;

namespace HumanResources.Entity.Entities
{
    public class Birim : BaseEntity
    {
        public string Ad { get; set; }

        // Baðlý Olduðu Departman (int olarak güncellendi)
        public int DepartmanId { get; set; }
        public Departman Departman { get; set; }

        public IList<AppUser> Personeller { get; set; }
    }
}
