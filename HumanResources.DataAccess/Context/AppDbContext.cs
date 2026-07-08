using HumanResources.Entity.Entities;
using HumanResources.Entity.Entities.Common;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HumanResources.DataAccess.Context
{
    public class AppDbContext(DbContextOptions options) : IdentityDbContext<AppUser, AppRole, int>(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var entitiyType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(IAuditEntity).IsAssignableFrom(entitiyType.ClrType))
                {
                    modelBuilder.Entity(entitiyType.ClrType)
                                .HasQueryFilter(ConvertToDeleteFilter(entitiyType.ClrType));
                }
            }

            modelBuilder.Entity<AppUser>()
                .HasOne(u => u.Departman)          // AppUser'ýn bir Departmaný vardýr
                .WithMany(d => d.Personeller)      // Departmanýn çok personeli vardýr
                .HasForeignKey(u => u.DepartmanId) // FK: DepartmanId
                .OnDelete(DeleteBehavior.Restrict); // Cascade delete'i kapat

            modelBuilder.Entity<Departman>()
                .HasOne(d => d.Yonetici)           // Departmanýn bir yöneticisi vardýr
                .WithMany()                        // Yöneticinin yönettikleri diye AppUser'da bir liste yok, boþ býrakkk
                .HasForeignKey(d => d.YoneticiId)  // FK: YoneticiId
                .OnDelete(DeleteBehavior.Restrict); // Cascade delete'i kapat

            modelBuilder.Entity<AppUser>()
                .HasOne(u => u.Amir)
                .WithMany(u => u.BagliPersoneller)
                .HasForeignKey(u => u.AmirId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AppUser>()
     .HasOne(u => u.Vardiya)
     .WithMany(v => v.Personeller)
     .HasForeignKey(u => u.VardiyaId)
     .OnDelete(DeleteBehavior.SetNull);

            // 2) Vardiya -> Yonetici (bir vardiyanýn bir yöneticisi olur, YoneticiId üzerinden)
            // Bu, yukarýdakinden TAMAMEN AYRI bir iliþki — AppUser tarafýnda ters navigation yok
            modelBuilder.Entity<Vardiya>()
                .HasOne(v => v.Yonetici)
                .WithMany()
                .HasForeignKey(v => v.YoneticiId)
                .OnDelete(DeleteBehavior.SetNull);

        }

        private static LambdaExpression ConvertToDeleteFilter(Type type)
        {
            var parameter = Expression.Parameter(type, "e");

            var property = Expression.Property(Expression.Convert(parameter, typeof(IAuditEntity)), "SilindiMi");

            var notDeleted = Expression.Not(property);

            return Expression.Lambda(notDeleted, parameter);
        }

        public DbSet<Departman> Departmanlar { get; set; }
        public DbSet<Birim> Birimler { get; set; }

        public DbSet<Izin> Izinler { get; set; }
        public DbSet<IzinTuru> IzinTurleri { get; set; }

        public DbSet<Zimmet> Zimmetler { get; set; }
        public DbSet<ZimmetTuru> ZimmetTurleri { get; set; }

        public DbSet<Egitim> Egitimler { get; set; }
        public DbSet<AppUserEgitim> AppUserEgitimler { get; set; } // Çoka çok (Many-to-Many) veya ara tablo
        public DbSet<Sertifika> Sertifikalar { get; set; }
        public DbSet<SertifikaTuru> SertifikaTurleri { get; set; }

        public DbSet<Vardiya> Vardiyalar { get; set; }

        public DbSet<DisiplinKaydi> DisiplinKayitlari { get; set; }















    }
}
