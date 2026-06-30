using HumanResources.DataAccess.Context;
using HumanResources.Entity.Entities;

namespace HumanResources.DataAccess.Repositories.PermissionRepositories
{
    public class PermissionRepository : GenericRepository<Izin>, IPermissionRepository
    {
        public PermissionRepository(AppDbContext _context) : base(_context)
        {
        }
    }
}
