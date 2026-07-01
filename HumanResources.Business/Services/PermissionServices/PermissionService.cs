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
                var entity = await _permissionRepository.GetByIdAsync(id);

                if (entity is null)
                {
                    return BaseResult<object>.Fail("Permission Not Found");
                }

                _permissionRepository.Delete(entity);

                bool result = await _unitOfWork.SaveChangesAsync();

                return result ? BaseResult<object>.Success() : BaseResult<object>.Fail("Deleted Failed");
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
            var entity = await _permissionRepository.GetByIdAsync(approveDto.Id);

            if (entity == null) return BaseResult<object>.Fail("Ýzin kaydý bulunamadý.");

            // ÝK doðrudan onaylayamaz, önce Amirin onaylamýþ olmasý (true) gerekir!
            if (entity.AmirOnayi != true)
            {
                return BaseResult<object>.Fail("Bu izin henüz amir tarafýndan onaylanmamýþ!");
            }

            // Sadece ÝK onayýný güncelliyoruz
            entity.IkOnayi = approveDto.OnayDurumu;

            _permissionRepository.Update(entity);
            bool result = await _unitOfWork.SaveChangesAsync();

            return result ? BaseResult<object>.Success("ÝK onayý baþarýyla kaydedildi.") : BaseResult<object>.Fail("Ýþlem baþarýsýz.");
        }
















    }
    }