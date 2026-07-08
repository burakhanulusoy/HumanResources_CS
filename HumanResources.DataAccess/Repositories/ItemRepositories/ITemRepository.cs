using HumanResources.DataAccess.Context;
using HumanResources.Entity.Entities;
using Microsoft.EntityFrameworkCore;

namespace HumanResources.DataAccess.Repositories.ItemRepositories
{
    public class ItemRepository : GenericRepository<Zimmet>, IItemRepository
    {
        public ItemRepository(AppDbContext _context) : base(_context)
        {
        }

        public async Task<List<Zimmet>> GetAllItemsWithDetailsAsync()
        {
            return await _table
                .Include(x => x.AppUser)    // Eşyanın kimde olduğunu dahil et
                .Include(x => x.ZimmetTuru) // Eşyanın katalog bilgisini (türünü) dahil et
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<Zimmet>> GetItemsByUserIdAsync(int userId)
        {
            return await _table
                .Include(x => x.ZimmetTuru) // Personel sadece ne tür bir eşya aldığını görsün
                .Where(x => x.AppUserId == userId)
                .AsNoTracking()
                .ToListAsync();
        }

     public async Task<Zimmet> GetItemWithDetailsByIdAsync(int id)
{
    return await _table
        .Include(x => x.AppUser)
            .ThenInclude(u => u.Amir)   // YENİ — amir bilgisini de getir
        .Include(x => x.AppUser)
            .ThenInclude(u => u.Departman)
        .Include(x => x.AppUser)
            .ThenInclude(u => u.Birim)
        .Include(x => x.ZimmetTuru)
        .AsNoTracking()
        .FirstOrDefaultAsync(x => x.Id == id);
}
    }
}