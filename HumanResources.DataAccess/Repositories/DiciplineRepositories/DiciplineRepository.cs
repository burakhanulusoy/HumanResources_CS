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
                .Include(x => x.AppUser) // Personelin detay bilgileri de gelsin
                .Where(x => x.AppUserId == userId)
                .AsNoTracking() // Sadece okuma yapacaðýmýz için performansý artýrýr
                .ToListAsync();
        }
    }
}