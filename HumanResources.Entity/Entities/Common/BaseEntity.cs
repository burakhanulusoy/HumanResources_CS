namespace HumanResources.Entity.Entities.Common
{
    public abstract class BaseEntity : IAuditEntity
    {
        public int Id { get; set; }
        public DateTime OlusturulmaTarihi { get; set; }
        public DateTime GuncellenmeTarihi { get; set; }
        public bool SilindiMi { get; set; } = false;
    }
}
