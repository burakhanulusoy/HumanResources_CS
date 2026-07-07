using HumanResources.DataAccess.Context;
using HumanResources.Entity.Entities;
using Microsoft.EntityFrameworkCore;

namespace HumanResources.DataAccess.Repositories.DepartmentRepositories
{
    public class DepartmentRepository : GenericRepository<Departman>, IDepartmentRepository
    {
        public DepartmentRepository(AppDbContext _context) : base(_context)
        {
        }

        public Task<List<Departman>> GetDepartmentsWithUserAsync()
        {
            return _table.Include(x => x.Yonetici)
                         .Include(x => x.Birimler)
                         .Include(x => x.Personeller)
                         .AsNoTracking()
                         .ToListAsync();
        }

        public Task<Departman> GetDepartmentWithUserAsync(int id)
        {
            return _table.Include(x => x.Yonetici)
                         .Include(x => x.Personeller)
                             .ThenInclude(p => p.Birim)
                         .AsNoTracking()
                         .FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<Departman> GetDepartmentWithUnitsAsync(int id)
        {
            return _table.Include(x => x.Yonetici)
                         .Include(x => x.Birimler)
                             .ThenInclude(b => b.Personeller)   // her birimin kisi sayisi icin
                         .AsNoTracking()
                         .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
