using HumanResources.DataAccess.Context;
using HumanResources.Entity.Entities;
using HumanResources.Entity.Enums;
using Microsoft.EntityFrameworkCore;

namespace HumanResources.DataAccess.Repositories.UserEducationRepositories
{
    public class UserEducationRepository : GenericRepository<AppUserEgitim>, IUserEducationRepository
    {
        public UserEducationRepository(AppDbContext _context) : base(_context)
        {
        }

        public Task<List<AppUserEgitim>> GetApplicationStatusAsync(ApplicationStatus durum)
        {
            return _table
                .Include(x => x.AppUser) 
                .Include(x => x.Egitim)  // Eðitim bilgisini baðla
                .Where(x => x.BasvuruDurumu == durum)
                .AsNoTracking()
                .ToListAsync();
        }

        public Task<List<AppUserEgitim>> GetEducationByUserIdAsync(int userId)
        {
            return _table
                .Include(x => x.Egitim) // Eðitimin adýný, tarihini göstermek için
                .Where(x => x.AppUserId == userId)
                .AsNoTracking()
                .ToListAsync();
        }

        public Task<List<AppUserEgitim>> GetUserEducationWithAllInfoAsync()
        {
            return _table
                .Include(x => x.AppUser)
                .Include(x => x.Egitim)  // Eðitim bilgisini baðla
                .AsNoTracking()
                .ToListAsync();
        }

        public Task<List<AppUserEgitim>> GetUsersByEducationIdAsync(int egitimId)
        {
            return _table
                .Include(x => x.AppUser) // Katýlýmcýnýn adýný, sicilini göstermek için
                .Where(x => x.EgitimId == egitimId)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}