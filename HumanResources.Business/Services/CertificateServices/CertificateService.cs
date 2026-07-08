using FluentValidation;
using HumanResources.Business.Base;
using HumanResources.Business.DTOs.CertificateDtos;
using HumanResources.Business.Services.FileServices; // IFileService için eklendi
using HumanResources.DataAccess.Repositories.CertificateRepositories;
using HumanResources.DataAccess.UOW;
using HumanResources.Entity.Entities;
using HumanResources.Entity.Enums;
using Mapster;

namespace HumanResources.Business.Services.CertificateServices
{
    public class CertificateService : ICertificateService
    {
        private readonly ICertificateRepository _certificateRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<UpdateCertificateDto> _updateValidator;
        private readonly IValidator<CreateCertificateDto> _createValidator;
        private readonly IFileService _fileService; // Yeni eklenen servisimiz

        public CertificateService(ICertificateRepository certificateRepository,
                                  IUnitOfWork unitOfWork,
                                  IValidator<UpdateCertificateDto> updateValidator,
                                  IValidator<CreateCertificateDto> createValidator,
                                  IFileService fileService) // Constructor'a eklendi
        {
            _certificateRepository = certificateRepository;
            _unitOfWork = unitOfWork;
            _updateValidator = updateValidator;
            _createValidator = createValidator;
            _fileService = fileService;
        }

        public async Task<BaseResult<object>> CreateAsync(CreateCertificateDto createDto)
        {
            var validationResult = await _createValidator.ValidateAsync(createDto);

            if (!validationResult.IsValid)
            {
                return BaseResult<object>.Fail(validationResult.Errors);
            }

            var entity = createDto.Adapt<Sertifika>();

            if (createDto.Dosya != null)
            {
                string customName = $"Kullanici{createDto.AppUserId}_Tur{createDto.SertifikaTuruId}";
                entity.DosyaYolu = await _fileService.UploadFileAsync(createDto.Dosya, "certificates", customName);
            }

            entity.AlinmaTarihi = DateTime.SpecifyKind(entity.AlinmaTarihi, DateTimeKind.Utc);

            // ÝÞ KURALI: Süresiz iþaretlenmiþse tarihler önemsizdir, sabit uzak bir tarihe kilitlenir
            if (createDto.SuresizGecerli)
            {
                entity.Durumu = CertificateStatus.Sinirsiz;
                entity.GecerlilikTarihi = DateTime.SpecifyKind(new DateTime(9999, 12, 31), DateTimeKind.Utc);
                entity.YenilemeTarihi = DateTime.SpecifyKind(new DateTime(9999, 12, 31), DateTimeKind.Utc);
            }
            else
            {
                entity.Durumu = CertificateStatus.Gecerli;
                entity.GecerlilikTarihi = DateTime.SpecifyKind(entity.GecerlilikTarihi, DateTimeKind.Utc);
                entity.YenilemeTarihi = DateTime.SpecifyKind(entity.YenilemeTarihi, DateTimeKind.Utc);
            }

            await _certificateRepository.CreateAsync(entity);
            bool result = await _unitOfWork.SaveChangesAsync();

            return result ? BaseResult<object>.Success(entity) : BaseResult<object>.Fail("Created Failed");
        }

        public async Task<BaseResult<object>> UpdateAsync(UpdateCertificateDto updateDto)
        {
            var validationResult = await _updateValidator.ValidateAsync(updateDto);

            if (!validationResult.IsValid) return BaseResult<object>.Fail(validationResult.Errors);

            var entity = await _certificateRepository.GetByIdAsync(updateDto.Id);

            if (entity == null) return BaseResult<object>.Fail("Güncellenecek sertifika bulunamadý.");

            updateDto.Adapt(entity);

            if (updateDto.Dosya != null)
            {
                if (!string.IsNullOrEmpty(entity.DosyaYolu))
                {
                    _fileService.DeleteFile(entity.DosyaYolu);
                }

                string customName = $"Kullanici{updateDto.AppUserId}_Tur{updateDto.SertifikaTuruId}";
                entity.DosyaYolu = await _fileService.UploadFileAsync(updateDto.Dosya, "certificates", customName);
            }

            entity.AlinmaTarihi = DateTime.SpecifyKind(entity.AlinmaTarihi, DateTimeKind.Utc);

            // ÝÞ KURALI: Durum Süresiz seçildiyse tarihler sabit uzak tarihe kilitlenir
            if (entity.Durumu == CertificateStatus.Sinirsiz)
            {
                entity.GecerlilikTarihi = DateTime.SpecifyKind(new DateTime(9999, 12, 31), DateTimeKind.Utc);
                entity.YenilemeTarihi = DateTime.SpecifyKind(new DateTime(9999, 12, 31), DateTimeKind.Utc);
            }
            else
            {
                entity.GecerlilikTarihi = DateTime.SpecifyKind(entity.GecerlilikTarihi, DateTimeKind.Utc);
                entity.YenilemeTarihi = DateTime.SpecifyKind(entity.YenilemeTarihi, DateTimeKind.Utc);
            }

            _certificateRepository.Update(entity);
            bool result = await _unitOfWork.SaveChangesAsync();

            return result ? BaseResult<object>.Success(entity) : BaseResult<object>.Fail("Updated Failed");
        }



        public async Task<BaseResult<object>> DeleteAsync(int id)
        {
            var entity = await _certificateRepository.GetByIdAsync(id);

            if (entity is null)
            {
                return BaseResult<object>.Fail("Certificate Not Found");
            }

            // 2. KAYIT SÝLÝNÝRKEN SUNUCUDAKÝ DOSYAYI DA SÝL
            if (!string.IsNullOrEmpty(entity.DosyaYolu))
            {
                _fileService.DeleteFile(entity.DosyaYolu);
            }

            _certificateRepository.Delete(entity);
            bool result = await _unitOfWork.SaveChangesAsync();

            return result ? BaseResult<object>.Success() : BaseResult<object>.Fail("Deleted Failed");
        }

        public async Task<BaseResult<List<ResultCertificateDto>>> GetAllAsync()
        {
            var entities = await _certificateRepository.GetAllAsync();
            var mappedEntities = entities.Adapt<List<ResultCertificateDto>>();

            return BaseResult<List<ResultCertificateDto>>.Success(mappedEntities);
        }

        public async Task<BaseResult<ResultCertificateDto>> GetByIdAsync(int id)
        {
            var entity = await _certificateRepository.GetByIdAsync(id);

            if (entity is null) return BaseResult<ResultCertificateDto>.Fail("Certificate Not Found");

            var mappedEntity = entity.Adapt<ResultCertificateDto>();
            return BaseResult<ResultCertificateDto>.Success(mappedEntity);
        }

     

        // --- Özel Metotlar ---

        public async Task<BaseResult<List<CertificateDto>>> GetCertificateByUserIdAsync(int userId)
        {
            var entities = await _certificateRepository.GetCertificateByUserIdAsync(userId);

            // ÝÞ KURALI: Kayýt yoksa Fail dön
            if (entities == null || !entities.Any())
            {
                return BaseResult<List<CertificateDto>>.Fail("Bu personele ait herhangi bir sertifika kaydý bulunamadý.");
            }

            var mappedEntities = entities.Adapt<List<CertificateDto>>();
            return BaseResult<List<CertificateDto>>.Success(mappedEntities);
        }

        public async Task<BaseResult<List<CertificateDto>>> GetDateUpcamingSoonAsync(int bildirimGunu)
        {
            var entities = await _certificateRepository.GetDateUpcamingSoonAsync(bildirimGunu);

            if (entities == null || !entities.Any())
            {
                // Mesajý yeni duruma göre güncelledik
                return BaseResult<List<CertificateDto>>.Fail($"Dikkat gerektiren (süresi dolan, yaklaþan veya iptal edilen) sertifika bulunmamaktadýr.");
            }

            var mappedEntities = entities.Adapt<List<CertificateDto>>();
            return BaseResult<List<CertificateDto>>.Success(mappedEntities);
        }

        public async Task<BaseResult<List<CertificateDto>>> GetUsersByCertificateTypeIdAsync(int sertifikaTuruId)
        {
            var entities = await _certificateRepository.GetUsersByCertificateTypeIdAsync(sertifikaTuruId);

            // ÝÞ KURALI: Bu belgeyi kimse almamýþsa Fail dön
            if (entities == null || !entities.Any())
            {
                return BaseResult<List<CertificateDto>>.Fail("Bu sertifika türüne sahip herhangi bir personel bulunamadý.");
            }

            var mappedEntities = entities.Adapt<List<CertificateDto>>();
            return BaseResult<List<CertificateDto>>.Success(mappedEntities);
        }

        // CertificateService.cs — ekle
        public async Task<BaseResult<List<CertificateDto>>> GetAllWithInfoAsync()
        {
            var entities = await _certificateRepository.GetAllWithInfoAsync();
            var mappedEntities = entities.Adapt<List<CertificateDto>>();
            return BaseResult<List<CertificateDto>>.Success(mappedEntities);
        }

        public async Task<BaseResult<CertificateDto>> GetByIdWithInfoAsync(int id)
        {
            var entity = await _certificateRepository.GetByIdWithInfoAsync(id);

            if (entity is null) return BaseResult<CertificateDto>.Fail("Certificate Not Found");

            var mappedEntity = entity.Adapt<CertificateDto>();
            return BaseResult<CertificateDto>.Success(mappedEntity);
        }


    }
}