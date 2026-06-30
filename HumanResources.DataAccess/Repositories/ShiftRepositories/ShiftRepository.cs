using HumanResources.DataAccess.Context;
using HumanResources.Entity.Entities;

namespace HumanResources.DataAccess.Repositories.ShiftRepositories
{
    public class ShiftRepository : GenericRepository<Vardiya>, IShiftRepository
    {
        public ShiftRepository(AppDbContext _context) : base(_context)
        {
        }
    }
}
