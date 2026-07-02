    using FluentValidation;
    using HumanResources.Business.Base;
    using HumanResources.Business.DTOs.PermissionDtos;
    using HumanResources.DataAccess.Repositories.PermissionRepositories;
    using HumanResources.DataAccess.UOW;
    using HumanResources.Entity.Entities;
    using Mapster;

    namespace HumanResources.Business.Services.PermissionServices
    {
        public class PermissionService(IUnitOfWork _unitOfWork
                                      , IPermissionRepository _permissionRepository
                                      , IValidator<UpdatePermissionDto> _updateValidator
                                      , IValidator<CreatePermissionDto> _createValidator) : IPermissionService
        {
            public async Task<BaseResult<object>> CreateAsync(CreatePermissionDto createDto)
            {

                var validationResult = await _createValidator.ValidateAsync(createDto);

                if (!validationResult.IsValid)
                {
                    return BaseResult<object>.Fail(validationResult.Errors);
                }
                var entity = createDto.Adapt<Izin>();
            
                entity.BaslangicTarihi = DateTime.SpecifyKind(entity.BaslangicTarihi, DateTimeKind.Utc);
                entity.BitisTarihi = DateTime.SpecifyKind(entity.BitisTarihi, DateTimeKind.Utc);
               
                await _permissionRepository.CreateAsync(entity);

                bool result = await _unitOfWork.SaveChangesAsync();

                return result ? BaseResult<object>.Success(entity) : BaseResult<object>.Fail("Created Failed");
            }

        public async Task<BaseResult<object>> DeleteAsync(int id)
        {
            var entity = await _permissionRepository.GetPermissionWithUserAsync(id);
            if (entity == null) return BaseResult<object>.Fail("Ýzin kaydý bulunamadý.");

            // Sadece onaylanmýþ izinlerde gün iadesi yap
            if (entity.AmirOnayi == true && entity.IkOnayi == true)
            {
                DateTime today = DateTime.UtcNow.Date;
                DateTime start = entity.BaslangicTarihi.Date;
                DateTime end = entity.BitisTarihi.Date;

                int refundableDays = 0;

                if (start > today)
                {
                    // Ýzin henüz baþlamamýþ: Tüm günleri iade et
                    refundableDays = (end - start).Days + 1; // +1 mantýðý: 1'inden 1'ine 1 gün sürer.
                }
                else if (end >= today)
                {
                    // Ýzin þu an devam ediyor: Sadece bugünden sonrasýný iade et
                    // Örnek: Bugün 3'ü, bitiþ 5'i. 4 ve 5'i iade et.
                    refundableDays = (end - today).Days;
                }
                else
                {
                    // Ýzin geçmiþte kalmýþ: Ýade yok (Personel zaten iznini kullanmýþ)
                    refundableDays = 0;
                }

                if (refundableDays > 0)
                {
                    entity.Personel.ToplamKullanilanIzinGunu -= refundableDays;
                    if (entity.Personel.ToplamKullanilanIzinGunu < 0) entity.Personel.ToplamKullanilanIzinGunu = 0;
                }
            }

            _permissionRepository.Delete(entity);
            bool result = await _unitOfWork.SaveChangesAsync();

            return result ? BaseResult<object>.Success("Ýzin silindi.") : BaseResult<object>.Fail("Silme baþarýsýz.");
        }

        public async Task<BaseResult<List<PermissionDto>>> GetAllAsync()
            {
                var entities = await _permissionRepository.GetAllAsync();

                var mappedEntities = entities.Adapt<List<PermissionDto>>();

                return BaseResult<List<PermissionDto>>.Success(mappedEntities);
            }

            public async Task<BaseResult<List<ResultPermissionDto>>> GetAllPermissionWithUser()
            {
                var entities = await _permissionRepository.GetAllPermissionWithUserAsync();

                var mappedEntities = entities.Adapt<List<ResultPermissionDto>>();

                return BaseResult<List<ResultPermissionDto>>.Success(mappedEntities);
            }

            public async Task<BaseResult<ResultPermissionDto>> GetPermissionWithUser(int id)
            {
                var entity = await _permissionRepository.GetPermissionWithUserAsync(id);

                if (entity is null)
                {
                    return BaseResult<ResultPermissionDto>.Fail("Permission Not Found");
                }

                var mappedEntity = entity.Adapt<ResultPermissionDto>();

                return BaseResult<ResultPermissionDto>.Success(mappedEntity);
            }

            public async Task<BaseResult<PermissionDto>> GetByIdAsync(int id)
            {
                var entity = await _permissionRepository.GetByIdAsync(id);

                if (entity is null)
                {
                    return BaseResult<PermissionDto>.Fail("Permission Not Found");
                }

                var mappedEntity = entity.Adapt<PermissionDto>();

                return BaseResult<PermissionDto>.Success(mappedEntity);
            }

            public async Task<BaseResult<object>> UpdateAsync(UpdatePermissionDto updateDto)
            {
                var validationResult = await _updateValidator.ValidateAsync(updateDto);

                if (!validationResult.IsValid)
                {
                    return BaseResult<object>.Fail(validationResult.Errors);
                }

                // DTO içerisinde BaseDto'dan gelen bir Id property'si olduðu varsayýlmýþtýr.
                var entity = await _permissionRepository.GetByIdAsync(updateDto.Id);

                if (entity == null)
                    return BaseResult<object>.Fail("Güncellenecek kayýt bulunamadý.");

                updateDto.Adapt(entity);

            entity.BaslangicTarihi = DateTime.SpecifyKind(entity.BaslangicTarihi, DateTimeKind.Utc);
            entity.BitisTarihi = DateTime.SpecifyKind(entity.BitisTarihi, DateTimeKind.Utc);

            _permissionRepository.Update(entity);

                bool result = await _unitOfWork.SaveChangesAsync();

                return result ? BaseResult<object>.Success(entity) : BaseResult<object>.Fail("Updated Failed");
            }








        public async Task<BaseResult<List<ResultPermissionDto>>> GetMyTeamPendingPermissionsAsync(int amirId)
        {
            var entities = await _permissionRepository.GetMyTeamPendingPermissionsAsync(amirId);
            var mappedEntities = entities.Adapt<List<ResultPermissionDto>>();
            return BaseResult<List<ResultPermissionDto>>.Success(mappedEntities);
        }

        public async Task<BaseResult<List<ResultPermissionDto>>> GetIkPendingPermissionsAsync()
        {
            var entities = await _permissionRepository.GetIkPendingPermissionsAsync();
            var mappedEntities = entities.Adapt<List<ResultPermissionDto>>();
            return BaseResult<List<ResultPermissionDto>>.Success(mappedEntities);
        }





        // AMÝR ONAY ÝÞLEMÝ
        public async Task<BaseResult<object>> ApproveByAmirAsync(ApprovePermissionDto approveDto)
        {
            var entity = await _permissionRepository.GetByIdAsync(approveDto.Id);

            if (entity == null) return BaseResult<object>.Fail("Ýzin kaydý bulunamadý.");

            // Sadece Amir onayýný güncelliyoruz, diðer hiçbir veriye dokunmuyoruz
            entity.AmirOnayi = approveDto.OnayDurumu;

            _permissionRepository.Update(entity);
            bool result = await _unitOfWork.SaveChangesAsync();

            return result ? BaseResult<object>.Success("Amir onayý baþarýyla kaydedildi.") : BaseResult<object>.Fail("Ýþlem baþarýsýz.");
        }




        // ÝK ONAY ÝÞLEMÝ
        public async Task<BaseResult<object>> ApproveByIkAsync(ApprovePermissionDto approveDto)
        {
            var entity = await _permissionRepository.GetPermissionWithUserAsync(approveDto.Id);

            if (entity == null) return BaseResult<object>.Fail("Ýzin kaydý bulunamadý.");

            // ÝK doðrudan onaylayamaz, önce Amirin onaylamýþ olmasý (true) gerekir!
            if (entity.AmirOnayi != true)
            {
                return BaseResult<object>.Fail("Bu izin henüz amir tarafýndan onaylanmamýþ!");
            }

            if (approveDto.OnayDurumu == true)
            {
                // Ýzin gününü hesapla (Bitis - Baslangic)
                // Eðer izin tek günse .Days 0 dönebilir, genellikle +1 eklenir ama 
                // sen direkt farký almak istersen (entity.BitisTarihi - entity.BaslangicTarihi).Days yeterli.
                int kullanilanGun = (entity.BitisTarihi.Date - entity.BaslangicTarihi.Date).Days;

                // Eðer izin gününü "0" bulursa (ayný gün izin gibi), en az 1 yapabilirsin:
                if (kullanilanGun <= 0) kullanilanGun = 1;

                // Personel nesnesini güncelliyoruz
                // EF Core bu entity'yi ve altýndaki Personel'i takip ettiði için
                // SaveChangesAsync ile otomatik olarak veritabanýna yansýyacak.
                entity.Personel.ToplamKullanilanIzinGunu += kullanilanGun;
            }

            entity.IkOnayi = approveDto.OnayDurumu;

            _permissionRepository.Update(entity);
            bool result = await _unitOfWork.SaveChangesAsync();

            return result ? BaseResult<object>.Success("ÝK onayý baþarýyla kaydedildi ve izin günleri güncellendi.") : BaseResult<object>.Fail("Ýþlem baþarýsýz.");
        }




        public async Task<BaseResult<List<ResultPermissionDto>>> GetByUserIdAsync(int userId)
        {
            var entities = await _permissionRepository.GetByUserIdAsync(userId);

            // ÝÞ KURALI: Defansif Programlama - Kayýt yoksa boþ liste yerine net bir mesajla Fail dönüyoruz.
            if (entities == null || !entities.Any())
            {
                return BaseResult<List<ResultPermissionDto>>.Fail("Bu personele ait herhangi bir izin geçmiþi bulunamadý.");
            }

            var mappedEntities = entities.Adapt<List<ResultPermissionDto>>();

            return BaseResult<List<ResultPermissionDto>>.Success(mappedEntities);
        }











    }
    }