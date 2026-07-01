using HumanResources.DataAccess.Context;
using HumanResources.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HumanResources.DataAccess.Repositories.ItemTypeRepositories
{
    public class ItemTypeRepository : GenericRepository<ZimmetTuru>, IItemTypeRepository
    {
        public ItemTypeRepository(AppDbContext _context) : base(_context)
        {
        }

        public async Task<List<ZimmetTuru>> GetAllItemTypesWithItemsAsync()
        {
            return await _table
                .Include(x => x.Zimmetler) // Bu türe ait tüm eþyalarý dahil et
                    .ThenInclude(z => z.AppUser) // O eþyanýn HANGÝ PERSONELDE olduðunu da dahil et
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<ZimmetTuru> GetItemTypeWithItemsByIdAsync(int id)
        {
            return await _table
                .Include(x => x.Zimmetler)
                    .ThenInclude(z => z.AppUser) // Hangi personelde olduðunu görmek için User verisini baðlýyoruz
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}