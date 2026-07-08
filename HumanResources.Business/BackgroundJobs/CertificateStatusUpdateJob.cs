using HumanResources.DataAccess.Repositories.CertificateRepositories;
using HumanResources.DataAccess.UOW;
using HumanResources.Entity.Enums;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace HumanResources.Business.BackgroundJobs
{
    public class CertificateStatusUpdateJob(IServiceScopeFactory _scopeFactory,
                                             ILogger<CertificateStatusUpdateJob> _logger) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await GuncelleAsync();

                // Her gün bir kere çalýþsýn
                await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
            }
        }

        private async Task GuncelleAsync()
        {
            using var scope = _scopeFactory.CreateScope();
            var certificateRepository = scope.ServiceProvider.GetRequiredService<ICertificateRepository>();
            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

            var suresiGecenler = await certificateRepository.GetExpiredButStillValidAsync();

            if (suresiGecenler.Count == 0)
            {
                _logger.LogInformation("Süresi dolan sertifika bulunamadý.");
                return;
            }

            foreach (var sertifika in suresiGecenler)
            {
                sertifika.Durumu = CertificateStatus.SuresiDolu;
                certificateRepository.Update(sertifika);
            }

            await unitOfWork.SaveChangesAsync();
            _logger.LogInformation("{Adet} sertifikanýn durumu 'Süresi Dolu' olarak güncellendi.", suresiGecenler.Count);
        }
    }
}