using HumanResources.Entity.Entities.Common;

namespace HumanResources.Entity.Entities
{
    public class Vardiya : BaseEntity 
    {
        public string Aciklama { get; set; } 

        public TimeSpan BaslangicSaati { get; set; }
        public TimeSpan BitisSaati { get; set; }

        public int AraDinlenmeSuresiDk { get; set; }

        public TimeSpan CalismaSuresi { get; set; }

       

        public IList<AppUser> Personeller { get; set; }
    }
}
