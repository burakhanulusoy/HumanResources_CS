using HumanResources.Entity.Entities;

namespace HumanResources.DataAccess.Repositories.CertificateTypeRepositories
{
    public interface ICertificateTypeRepository:IGenericRepository<SertifikaTuru>
    {
        Task<List<SertifikaTuru>> GetAllCertificateTypeWithCertificate();
        Task<SertifikaTuru> GetCertificateTypeWithCertificate(int id);



    }
}
