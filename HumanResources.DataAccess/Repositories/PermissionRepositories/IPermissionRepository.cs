using HumanResources.DataAccess.Repositories;
using HumanResources.Entity.Entities;

namespace HumanResources.DataAccess.Repositories.PermissionRepositories
{
    public interface IPermissionRepository : IGenericRepository<Izin>
    {
        Task<List<Izin>> GetAllPermissionWithUserAsync();
        Task<Izin> GetPermissionWithUserAsync(int id);

        // Amirin sadece kendi ekibindeki bekleyen izinleri görmesi
        Task<List<Izin>> GetMyTeamPendingPermissionsAsync(int amirId);

        //  İK'nın amirden geçmiş ama henüz İK tarafından onaylanmamış izinleri görmesi
        Task<List<Izin>> GetIkPendingPermissionsAsync();
    }
}