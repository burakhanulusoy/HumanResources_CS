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
                .Include(x => x.AppUser)
                    .ThenInclude(u => u.Departman)
                .Include(x => x.AppUser)
                    .ThenInclude(u => u.Birim)
                .Include(x => x.AppUser)
                    .ThenInclude(u => u.Amir)
                .Include(x => x.Demirbas)
                    .ThenInclude(d => d.ZimmetTuru)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<Zimmet>> GetItemsByUserIdAsync(int userId)
        {
            return await _table
                .Include(x => x.Demirbas)
                    .ThenInclude(d => d.ZimmetTuru)
                .Where(x => x.AppUserId == userId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Zimmet> GetItemWithDetailsByIdAsync(int id)
        {
            return await _table
                .Include(x => x.AppUser)
                    .ThenInclude(u => u.Amir)
                .Include(x => x.AppUser)
                    .ThenInclude(u => u.Departman)
                .Include(x => x.AppUser)
                    .ThenInclude(u => u.Birim)
                .Include(x => x.Demirbas)
                    .ThenInclude(d => d.ZimmetTuru)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}