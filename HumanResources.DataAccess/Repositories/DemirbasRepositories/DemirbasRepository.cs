using HumanResources.DataAccess.Context;
using HumanResources.Entity.Entities;
using HumanResources.Entity.Enums;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HumanResources.DataAccess.Repositories.DemirbasRepositories
{
    public class DemirbasRepository : GenericRepository<Demirbas>, IDemirbasRepository
    {
        private readonly AppDbContext _appContext;   // kendi referansýmýz

        public DemirbasRepository(AppDbContext context) : base(context)
        {
            _appContext = context;
        }

        public async Task<List<Demirbas>> GetAllWithTypeAsync()
        {
            return await _table
                .Include(x => x.ZimmetTuru)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<Demirbas>> GetAvailableAsync()
        {
            return await _table
                .Include(x => x.ZimmetTuru)
                .Where(x => x.Durumu == DemirbasDurumu.Musait)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Demirbas> GetByIdWithTypeAsync(int id)
        {
            return await _table
                .Include(x => x.ZimmetTuru)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> HasAnyZimmetAsync(int demirbasId)
        {
            return await _appContext.Set<Zimmet>()
                .AnyAsync(z => z.DemirbasId == demirbasId);
        }
    }
}