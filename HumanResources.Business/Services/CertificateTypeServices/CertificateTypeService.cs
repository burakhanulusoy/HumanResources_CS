using FluentValidation;
using HumanResources.Business.Base;
using HumanResources.Business.DTOs.CertificateTypeDtos;
using HumanResources.DataAccess.Repositories.CertificateTypeRepositories;
using HumanResources.DataAccess.UOW;
using HumanResources.Entity.Entities;
using Mapster;

namespace HumanResources.Business.Services.CertificateTypeServices
{
    public class CertificateTypeService(ICertificateTypeRepository _certificateTypeRepository,
                                        IUnitOfWork _unitOfWork,
                                        IValidator<UpdateCertificateTypeDto> _updateValidator,
                                        IValidator<CreateCertificateTypeDto> _createValidator) : ICertificateTypeService
    {
        public async Task<BaseResult<object>> CreateAsync(CreateCertificateTypeDto createDto)
        {
            var validationResult = await _createValidator.ValidateAsync(createDto);

            if (!validationResult.IsValid)
            {
                return BaseResult<object>.Fail(validationResult.Errors);
            }

            var entity = createDto.Adapt<SertifikaTuru>();

            await _certificateTypeRepository.CreateAsync(entity);
            bool result = await _unitOfWork.SaveChangesAsync();

            return result ? BaseResult<object>.Success(entity) : BaseResult<object>.Fail("Created Failed");
        }

        public async Task<BaseResult<object>> DeleteAsync(int id)
        {
            var entity = await _certificateTypeRepository.GetByIdAsync(id);

            if (entity is null)
            {
                return BaseResult<object>.Fail("Certificate Type Not Found");
            }

            _certificateTypeRepository.Delete(entity);
            bool result = await _unitOfWork.SaveChangesAsync();

            return result ? BaseResult<object>.Success() : BaseResult<object>.Fail("Deleted Failed");
        }

        public async Task<BaseResult<List<ResultCertificateTypeDto>>> GetAllAsync()
        {
            var entities = await _certificateTypeRepository.GetAllAsync();
            var mappedEntities = entities.Adapt<List<ResultCertificateTypeDto>>();

            return BaseResult<List<ResultCertificateTypeDto>>.Success(mappedEntities);
        }

        public async Task<BaseResult<ResultCertificateTypeDto>> GetByIdAsync(int id)
        {
            var entity = await _certificateTypeRepository.GetByIdAsync(id);

            if (entity is null)
                return BaseResult<ResultCertificateTypeDto>.Fail("Certificate Type Not Found");

            var mappedEntity = entity.Adapt<ResultCertificateTypeDto>();
            return BaseResult<ResultCertificateTypeDto>.Success(mappedEntity);
        }

        public async Task<BaseResult<object>> UpdateAsync(UpdateCertificateTypeDto updateDto)
        {
            var validationResult = await _updateValidator.ValidateAsync(updateDto);

            if (!validationResult.IsValid)
                return BaseResult<object>.Fail(validationResult.Errors);

            var entity = await _certificateTypeRepository.GetByIdAsync(updateDto.Id);

            if (entity == null)
                return BaseResult<object>.Fail("Güncellenecek sertifika türü bulunamadý.");

            updateDto.Adapt(entity);

            _certificateTypeRepository.Update(entity);
            bool result = await _unitOfWork.SaveChangesAsync();

            return result ? BaseResult<object>.Success(entity) : BaseResult<object>.Fail("Updated Failed");
        }








        public async Task<BaseResult<List<CertificateTypeDto>>> GetAllCertificateTypeWithCertificateAsync()
        {
            var entities = await _certificateTypeRepository.GetAllCertificateTypeWithCertificate();
            var mappedEntities = entities.Adapt<List<CertificateTypeDto>>();

            return BaseResult<List<CertificateTypeDto>>.Success(mappedEntities);
        }

        public async Task<BaseResult<CertificateTypeDto>> GetCertificateTypeWithCertificateAsync(int id)
        {
            var entity = await _certificateTypeRepository.GetCertificateTypeWithCertificate(id);

            if (entity is null)
                return BaseResult<CertificateTypeDto>.Fail("Certificate Type Not Found");

            var mappedEntity = entity.Adapt<CertificateTypeDto>();
            return BaseResult<CertificateTypeDto>.Success(mappedEntity);
        }
    }
}