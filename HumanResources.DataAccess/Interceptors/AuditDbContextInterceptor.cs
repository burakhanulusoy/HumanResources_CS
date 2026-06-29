using HumanResources.Entity.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace HumanResources.DataAccess.Interceptors
{
    public class AuditDbContextInterceptor : SaveChangesInterceptor
    {
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            foreach (var entry in eventData.Context.ChangeTracker.Entries())
            {
                // DEÐÝÞÝKLÝK BURADA: BaseEntity yerine IAuditEntity arýyoruz
                if (entry.Entity is not IAuditEntity auditEntity)
                {
                    continue;
                }

                if (entry.State is not EntityState.Added and not EntityState.Modified and not EntityState.Deleted)
                {
                    continue;
                }

                if (entry.State is EntityState.Added)
                {
                    // baseEntity yerine auditEntity kullanýyoruz
                    eventData.Context.Entry(auditEntity).Property(x => x.OlusturulmaTarihi).CurrentValue = DateTime.UtcNow;
                    eventData.Context.Entry(auditEntity).Property(x => x.GuncellenmeTarihi).IsModified = false;
                }

                if (entry.State is EntityState.Modified)
                {
                    eventData.Context.Entry(auditEntity).Property(x => x.GuncellenmeTarihi).CurrentValue = DateTime.UtcNow;
                    eventData.Context.Entry(auditEntity).Property(x => x.OlusturulmaTarihi).IsModified = false;
                }

                if (entry.State is EntityState.Deleted)
                {
                    entry.State = EntityState.Modified;
                    eventData.Context.Entry(auditEntity).Property(x => x.SilindiMi).CurrentValue = true;
                    eventData.Context.Entry(auditEntity).Property(x => x.OlusturulmaTarihi).IsModified = false;
                    eventData.Context.Entry(auditEntity).Property(x => x.GuncellenmeTarihi).CurrentValue = DateTime.UtcNow;
                }
            }

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
}
