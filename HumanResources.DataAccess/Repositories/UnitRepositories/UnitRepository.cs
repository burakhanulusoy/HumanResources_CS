using HumanResources.DataAccess.Context;
using HumanResources.Entity.Entities;
using Microsoft.EntityFrameworkCore;

namespace HumanResources.DataAccess.Repositories.UnitRepositories
{
    public class UnitRepository : GenericRepository<Birim>, IUnitRepository
    {
        public UnitRepository(AppDbContext _context) : base(_context)
        {
        }

        public Task<Birim> GetUnitWithUsersAsync(int unitId)
        {
            return _table.Include(u => u.Personeller)
                         .Include(u => u.Departman)
                             .ThenInclude(d => d.Yonetici)
                         .AsNoTracking()
                         .FirstOrDefaultAsync(u => u.Id == unitId);
        }
    }
}
