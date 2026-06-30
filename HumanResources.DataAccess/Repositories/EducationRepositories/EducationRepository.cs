using HumanResources.DataAccess.Context;
using HumanResources.Entity.Entities;
using Microsoft.EntityFrameworkCore;

namespace HumanResources.DataAccess.Repositories.EducationRepositories
{
    public class EducationRepository : GenericRepository<Egitim>, IEducationRepository
    {
        public EducationRepository(AppDbContext _context) : base(_context)
        {
        }

        public Task<List<Egitim>> GetAllEducationWithUserAsync()
        {
            return _table
                .Include(x => x.Katilimcilar)
                .ThenInclude(x => x.AppUser) // Katýlýmcýnýn kimlik bilgilerini de getir
                .AsNoTracking()
                .ToListAsync();
        }

        public Task<Egitim> GetEducationWithUserAsync(int id)
        {
            return _table
                .Include(x => x.Katilimcilar)
                .ThenInclude(x => x.AppUser) 
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}