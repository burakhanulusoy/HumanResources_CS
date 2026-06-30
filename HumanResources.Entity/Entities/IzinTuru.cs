using HumanResources.Entity.Entities.Common;

namespace HumanResources.Entity.Entities
{
    public class IzinTuru : BaseEntity
    {
        public string Ad { get; set; } 

        public bool UcretliMi { get; set; } = true;

        public IList<Izin> Izinler { get; set; }
    }
}