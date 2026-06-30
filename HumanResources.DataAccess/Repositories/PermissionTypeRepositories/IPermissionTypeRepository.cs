using HumanResources.Entity.Entities;

namespace HumanResources.DataAccess.Repositories.PermissionTypeRepositories
{
    public interface IPermissionTypeRepository :IGenericRepository<IzinTuru>
    {
        Task<List<IzinTuru>> GetAllPermissionTypeWithPermissions();
        Task<IzinTuru> GetPermissionTypeWithPermissions(int id);


    }
}
