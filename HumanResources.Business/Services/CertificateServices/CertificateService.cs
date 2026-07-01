using FluentValidation;
using HumanResources.Business.Base;
using HumanResources.Business.DTOs.CertificateDtos;
using HumanResources.DataAccess.Repositories.CertificateRepositories;
using HumanResources.DataAccess.UOW;
using HumanResources.Entity.Entities;
using HumanResources.Entity.Enums;
using Mapster;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HumanResources.Business.Services.CertificateServices
{
    public class CertificateService(ICertificateRepository _certificateRepository,
                                    IUnitOfWork _unitOfWork,
                                    IValidator<UpdateCertificateDto> _updateValidator,
                                    IValidator<CreateCertificateDto> _createValidator) : ICertificateService
    {
        public async Task<BaseResult<object>> CreateAsync(CreateCertificateDto createDto)
        {
            var validationResult = await _createValidator.ValidateAsync(createDto);

            if (!validationResult.IsValid)
            {
                return BaseResult<object>.Fail(validationResult.Errors);
            }

            var entity = createDto.Adapt<Sertifika>();

            entity.AlinmaTarihi = DateTime.SpecifyKind(entity.AlinmaTarihi, DateTimeKind.Utc);
            entity.GecerlilikTarihi = DateTime.SpecifyKind(entity.GecerlilikTarihi, DateTimeKind.Utc);
            entity.YenilemeTarihi = DateTime.SpecifyKind(entity.YenilemeTarihi, DateTimeKind.Utc);

            entity.Durumu = CertificateStatus.Gecerli;

            await _certificateRepository.CreateAsync(entity);
            bool result = await _unitOfWork.SaveChangesAsync();

            return result ? BaseResult<object>.Success(entity) : BaseResult<object>.Fail("Created Failed");
        }

        public async Task<BaseResult<object>> DeleteAsync(int id)
        {
            var entity = await _certificateRepository.GetByIdAsync(id);

            if (entity is null)
            {
                return BaseResult<object>.Fail("Certificate Not Found");
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

        public async Task<BaseResult<object>> UpdateAsync(UpdateCertificateDto updateDto)
        {
            var validationResult = await _updateValidator.ValidateAsync(updateDto);

            if (!validationResult.IsValid) return BaseResult<object>.Fail(validationResult.Errors);

            var entity = await _certificateRepository.GetByIdAsync(updateDto.Id);

            if (entity == null) return BaseResult<object>.Fail("Güncellenecek sertifika bulunamadý.");

            updateDto.Adapt(entity);

            entity.AlinmaTarihi = DateTime.SpecifyKind(entity.AlinmaTarihi, DateTimeKind.Utc);
            entity.GecerlilikTarihi = DateTime.SpecifyKind(entity.GecerlilikTarihi, DateTimeKind.Utc);
            entity.YenilemeTarihi = DateTime.SpecifyKind(entity.YenilemeTarihi, DateTimeKind.Utc);

            _certificateRepository.Update(entity);
            bool result = await _unitOfWork.SaveChangesAsync();

            return result ? BaseResult<object>.Success(entity) : BaseResult<object>.Fail("Updated Failed");
        }














        public async Task<BaseResult<List<CertificateDto>>> GetCertificateByUserIdAsync(int userId)
        {
            var entities = await _certificateRepository.GetCertificateByUserIdAsync(userId);
            var mappedEntities = entities.Adapt<List<CertificateDto>>();
            return BaseResult<List<CertificateDto>>.Success(mappedEntities);
        }

        public async Task<BaseResult<List<CertificateDto>>> GetDateUpcamingSoonAsync(int bildirimGunu)
        {
            var entities = await _certificateRepository.GetDateUpcamingSoonAsync(bildirimGunu);
            var mappedEntities = entities.Adapt<List<CertificateDto>>();
            return BaseResult<List<CertificateDto>>.Success(mappedEntities);
        }

        public async Task<BaseResult<List<CertificateDto>>> GetUsersByCertificateTypeIdAsync(int sertifikaTuruId)
        {
            var entities = await _certificateRepository.GetUsersByCertificateTypeIdAsync(sertifikaTuruId);
            var mappedEntities = entities.Adapt<List<CertificateDto>>();
            return BaseResult<List<CertificateDto>>.Success(mappedEntities);
        }
    }
}