using HumanResources.DataAccess.Context;
using HumanResources.Entity.Entities;

namespace HumanResources.DataAccess.Repositories.UnitRepositories
{
    public class UnitRepository : GenericRepository<Birim>, IUnitRepository
    {
        public UnitRepository(AppDbContext _context) : base(_context)
        {
        }
    }
}
