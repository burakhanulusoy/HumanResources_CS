using FluentValidation;
using HumanResources.Business.Base;
using HumanResources.Business.DTOs.PermissionTypeDtos;
using HumanResources.DataAccess.Repositories.PermissionTypeRepositories;
using HumanResources.DataAccess.UOW;
using HumanResources.Entity.Entities;
using Mapster;

namespace HumanResources.Business.Services.PermissionTypeServices
{
    public class PermissionTypeService(IUnitOfWork _unitOfWork
                                      , IPermissionTypeRepository _permissionTypeRepository
                                      , IValidator<UpdatePermissionTypeDto> _updateValidator
                                      , IValidator<CreatePermissionTypeDto> _createValidator) : IPermissionTypeService
    {
        public async Task<BaseResult<object>> CreateAsync(CreatePermissionTypeDto createDto)
        {
            var entity = createDto.Adapt<IzinTuru>();

            var validationResult = await _createValidator.ValidateAsync(createDto);

            if (!validationResult.IsValid)
            {
                return BaseResult<object>.Fail(validationResult.Errors);
            }

            await _permissionTypeRepository.CreateAsync(entity);

            bool result = await _unitOfWork.SaveChangesAsync();

            return result ? BaseResult<object>.Success(entity) : BaseResult<object>.Fail("Created Failed");
        }

        public async Task<BaseResult<object>> DeleteAsync(int id)
        {
            var entity = await _permissionTypeRepository.GetByIdAsync(id);

            if (entity is null)
            {
                return BaseResult<object>.Fail("PermissionType Not Found");
            }

            _permissionTypeRepository.Delete(entity);

            bool result = await _unitOfWork.SaveChangesAsync();

            return result ? BaseResult<object>.Success() : BaseResult<object>.Fail("Deleted Failed");
        }

        public async Task<BaseResult<List<ResultPermissionTypeDto>>> GetAllAsync()
        {
            var entities = await _permissionTypeRepository.GetAllAsync();

            var mappedEntities = entities.Adapt<List<ResultPermissionTypeDto>>();

            return BaseResult<List<ResultPermissionTypeDto>>.Success(mappedEntities);
        }

        public async Task<BaseResult<List<PermissionTypeDto>>> GetAllPermissionTypeWithPermissions()
        {
            var entities = await _permissionTypeRepository.GetAllPermissionTypeWithPermissions();

            var mappedEntities = entities.Adapt<List<PermissionTypeDto>>();

            return BaseResult<List<PermissionTypeDto>>.Success(mappedEntities);
        }

        public async Task<BaseResult<ResultPermissionTypeDto>> GetByIdAsync(int id)
        {
            var entity = await _permissionTypeRepository.GetByIdAsync(id);

            if (entity is null)
            {
                return BaseResult<ResultPermissionTypeDto>.Fail("PermissionType Not Found");
            }

            var mappedEntity = entity.Adapt<ResultPermissionTypeDto>();

            return BaseResult<ResultPermissionTypeDto>.Success(mappedEntity);
        }

        public async Task<BaseResult<PermissionTypeDto>> GetPermissionTypeWithPermissions(int id)
        {
            var entity = await _permissionTypeRepository.GetPermissionTypeWithPermissions(id);

            if (entity is null)
            {
                return BaseResult<PermissionTypeDto>.Fail("PermissionType Not Found");
            }

            var mappedEntity = entity.Adapt<PermissionTypeDto>();

            return BaseResult<PermissionTypeDto>.Success(mappedEntity);
        }

        public async Task<BaseResult<object>> UpdateAsync(UpdatePermissionTypeDto updateDto)
        {
            var validationResult = await _updateValidator.ValidateAsync(updateDto);

            if (!validationResult.IsValid)
            {
                return BaseResult<object>.Fail(validationResult.Errors);
            }

            var entity = await _permissionTypeRepository.GetByIdAsync(updateDto.Id);

            if (entity == null)
                return BaseResult<object>.Fail("Güncellenecek kayýt bulunamadý.");

            updateDto.Adapt(entity);

            _permissionTypeRepository.Update(entity);

            bool result = await _unitOfWork.SaveChangesAsync();

            return result ? BaseResult<object>.Success(entity) : BaseResult<object>.Fail("Updated Failed");
        }
    }
}