using HumanResources.DataAccess.Context;
using HumanResources.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HumanResources.DataAccess.Repositories.PermissionRepositories
{
    public class PermissionRepository : GenericRepository<Izin>, IPermissionRepository
    {
        public PermissionRepository(AppDbContext _context) : base(_context)
        {
        }

        public Task<List<Izin>> GetAllPermissionWithUserAsync()
        {
            return _table.Include(x => x.IzinTuru).Include(x => x.Personel).AsNoTracking().ToListAsync();
        }

        public Task<Izin> GetPermissionWithUserAsync(int id)
        {
            return _table.Include(x => x.IzinTuru).Include(x => x.Personel).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        // Amirin Ekraný (Sadece kendi adamlarý ve bekleyenler)
        public async Task<List<Izin>> GetMyTeamPendingPermissionsAsync(int amirId)
        {
            return await _table
                .Include(x => x.IzinTuru)
                .Include(x => x.Personel)
                // ÞART: Ýzni isteyen personelin amiri bu kiþi olmalý VE amir onayý henüz verilmemiþ (null) olmalý
                .Where(x => x.Personel.AmirId == amirId && x.AmirOnayi == null)
                .AsNoTracking()
                .ToListAsync();
        }

        //  ÝK'nýn Ekraný (Sadece amirden geçenler ve ÝK onayý bekleyenler)
        public async Task<List<Izin>> GetIkPendingPermissionsAsync()
        {
            return await _table
                .Include(x => x.IzinTuru)
                .Include(x => x.Personel)
                // ÞART: Amir onaylamýþ (true) olmalý VE ÝK onayý henüz verilmemiþ (null) olmalý
                .Where(x => x.AmirOnayi == true && x.IkOnayi == null)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<Izin>> GetByUserIdAsync(int userId)
        {
            return await _table
                .Include(x => x.IzinTuru) // UI'da izin türünün adýný (Yýllýk Ýzin, Rapor vb.) gösterebilmek için lazým
                .Where(x => x.PersonelId == userId)
                .AsNoTracking()
                .ToListAsync();
        }

    }
}