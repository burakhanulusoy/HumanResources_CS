using HumanResources.DataAccess.Context;
using HumanResources.Entity.Entities;
using Microsoft.EntityFrameworkCore;

namespace HumanResources.DataAccess.Repositories.CertificateTypeRepositories
{
    public class CertificateTypeRepository : GenericRepository<SertifikaTuru>, ICertificateTypeRepository
    {
        public CertificateTypeRepository(AppDbContext _context) : base(_context)
        {
        }

        public Task<List<SertifikaTuru>> GetAllCertificateTypeWithCertificate()
        {
            // Admin paneli listesi: Tüm sertifika türlerini ve kimlerin aldýðýný getirir
            return _table
                .Include(x => x.Sertifikalar)
                    .ThenInclude(s => s.AppUser) // Sertifikayý alan personellerin kimlik bilgileri
                .AsNoTracking()
                .ToListAsync();
        }

        public Task<SertifikaTuru> GetCertificateTypeWithCertificate(int id)
        {
            // Admin bir sertifikaya (Örn: Forklift) týkladýðýnda detaylarý ve alan personelleri getirir
            return _table
                .Include(x => x.Sertifikalar)
                    .ThenInclude(s => s.AppUser) // Sertifikayý alan personellerin kimlik bilgileri
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}