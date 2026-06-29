namespace HumanResources.Entity.Entities.Common
{
    public interface IAuditEntity
    {
        DateTime OlusturulmaTarihi { get; set; }
        DateTime GuncellenmeTarihi { get; set; }
        bool SilindiMi { get; set; }
    }
}
    