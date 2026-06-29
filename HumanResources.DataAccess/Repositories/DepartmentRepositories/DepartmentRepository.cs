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

      
    }
}
