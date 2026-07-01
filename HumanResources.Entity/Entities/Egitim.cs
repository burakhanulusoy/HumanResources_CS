using HumanResources.Entity.Entities.Common;
using HumanResources.Entity.Enums;

namespace HumanResources.Entity.Entities
{
    public class Egitim : BaseEntity 
    {
        public string Ad { get; set; } // Örn: IPC J-STD-001, ESD gibi !
        public string Egitmen { get; set; }
        public string EgitimAciklamasi { get; set; }
        public DateTime EgitimTarihi { get; set; }
        public int SuresiSaat { get; set; }
        public TrainingStatus Durumu { get; set; }

       
        // Ýliþki: Bu eðitime baþvuran/katýlan personeller
        public IList<AppUserEgitim> Katilimcilar { get; set; }
    }
}