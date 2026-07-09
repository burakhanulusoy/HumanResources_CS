using HumanResources.DataAccess.Context;
using HumanResources.Entity.Entities;
using Microsoft.EntityFrameworkCore;

namespace HumanResources.DataAccess.Repositories.DiciplineRepositories
{
    public class DiciplineRepository : GenericRepository<DisiplinKaydi>, IDiciplineRepository
    {
        public DiciplineRepository(AppDbContext _context) : base(_context)
        {
        }

        public async Task<List<DisiplinKaydi>> GetByUserIdAsync(int userId)
        {
            return await _table
                .Include(x => x.AppUser)
                    .ThenInclude(u => u.Departman)
                .Include(x => x.AppUser)
                    .ThenInclude(u => u.Birim)
                .Include(x => x.AppUser)
                    .ThenInclude(u => u.Vardiya)
                        .ThenInclude(v => v.Yonetici) // Personelin vardiya amirini de getir
                .Where(x => x.AppUserId == userId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<DisiplinKaydi>> GetAllWithUserAsync()
        {
            return await _table
                .Include(x => x.AppUser)
                    .ThenInclude(u => u.Departman)
                .Include(x => x.AppUser)
                    .ThenInclude(u => u.Birim)
                .Include(x => x.AppUser)
                    .ThenInclude(u => u.Vardiya)
                        .ThenInclude(v => v.Yonetici)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
