using HumanResources.DataAccess.Context;
using HumanResources.Entity.Entities;
using Microsoft.EntityFrameworkCore;

namespace HumanResources.DataAccess.Repositories.PermissionRepositories
{
    public class PermissionRepository : GenericRepository<Izin>, IPermissionRepository
    {
        public PermissionRepository(AppDbContext _context) : base(_context)
        {
        }

        public Task<List<Izin>> GetAllPermissionWithUserAsync()
        {
            return _table.Include(x=>x.IzinTuru).Include(x=>x.Personel).AsNoTracking().ToListAsync();
        }

        public Task<Izin> GetPermissionWithUserAsync(int id)
        {
            return _table.Include(x => x.IzinTuru).Include(x => x.Personel).AsNoTracking().FirstOrDefaultAsync(x=>x.Id==id);

        }
    }
}
