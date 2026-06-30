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
            return _table.Include(x => x.Yonetici).AsNoTracking().ToListAsync();
        }

        public Task<Departman> GetDepartmentWithUserAsync(int id)
        {
            return _table.Include(x=>x.Yonetici).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
