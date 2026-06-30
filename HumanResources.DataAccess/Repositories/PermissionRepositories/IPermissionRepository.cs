using HumanResources.Entity.Entities;

namespace HumanResources.DataAccess.Repositories.PermissionRepositories
{
    public interface IPermissionRepository :IGenericRepository<Izin>
    {
        Task<List<Izin>> GetAllPermissionWithUserAsync();
        Task<Izin> GetPermissionWithUserAsync(int id);



    }
}
