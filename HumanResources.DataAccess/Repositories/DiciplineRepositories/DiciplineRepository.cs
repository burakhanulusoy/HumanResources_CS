using HumanResources.DataAccess.Context;
using HumanResources.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
                    .ThenInclude(u => u.Departman) // Personelin Departman bilgisini getir
                .Include(x => x.AppUser)
                    .ThenInclude(u => u.Birim)     // Personelin Birim bilgisini getir
                .Where(x => x.AppUserId == userId)
                .AsNoTracking() // Sadece okuma yapacaðýmýz için performansý artýrýr
                .ToListAsync();
        }

        public async Task<List<DisiplinKaydi>> GetAllWithUserAsync()
        {
            return await _table
                .Include(x => x.AppUser)
                    .ThenInclude(u => u.Departman) // Listeleme ekranýnda da lazým olabilir diye buraya da ekledik
                .Include(x => x.AppUser)
                    .ThenInclude(u => u.Birim)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}