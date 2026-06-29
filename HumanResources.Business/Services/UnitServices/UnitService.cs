using FluentValidation;
using HumanResources.Business.Base;
using HumanResources.Business.DTOs.DepartmentDtos;
using HumanResources.Business.DTOs.UnitDtos;
using HumanResources.DataAccess.Repositories.UnitRepositories;
using HumanResources.DataAccess.UOW;
using HumanResources.Entity.Entities;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace HumanResources.Business.Services.UnitServices
{
    public class UnitService(IUnitRepository _unitReposirory
                             ,IUnitOfWork _unitOfWork
                             ,IValidator<CreateUnitDto> _createValidator
                             ,IValidator<UpdateUnitDto> _updateValidator) : IUnitService
    {
        public async Task<BaseResult<object>> CreateAsync(CreateUnitDto createDto)
        {
            var item = createDto.Adapt<Birim>();

            var validationResult = await _createValidator.ValidateAsync(createDto);

            if (!validationResult.IsValid)
            {
                return BaseResult<object>.Fail(validationResult.Errors);

            }

            await _unitReposirory.CreateAsync(item);

            var result = await _unitOfWork.SaveChangesAsync();

            return result ? BaseResult<object>.Success(item) : BaseResult<object>.Fail("Created Failed");


        }

        public async Task<BaseResult<object>> DeleteAsync(int id)
        {
            var item = await _unitReposirory.GetByIdAsync(id);

            if (item is null)
            {
                return BaseResult<object>.Fail("Department Not Found");
            }

            _unitReposirory.Delete(item);

            var result = await _unitOfWork.SaveChangesAsync();

            return result ? BaseResult<object>.Success() : BaseResult<object>.Fail("Deleted Failed");

        }

        public async Task<BaseResult<List<ResultUnitDto>>> GetAllAsync()
        {
            var query = _unitReposirory.GetQueryable();

            var units = await query.Select(b => new ResultUnitDto
            {
                Id = b.Id, 
                Ad = b.Ad,
                DepartmanId = b.DepartmanId,


                DepartmanAd = b.Departman.Ad,
                PersonellerCount = b.Personeller.Count()
            }).ToListAsync();

            return BaseResult<List<ResultUnitDto>>.Success(units);

        }

        public async Task<BaseResult<ResultUnitDto>> GetByIdAsync(int id)
        {
            var query = _unitReposirory.GetQueryable();

            var unit = await query
                .Where(b => b.Id == id)
                .Select(b => new ResultUnitDto
                {
                    Id = b.Id,
                    Ad = b.Ad,
                    DepartmanId = b.DepartmanId,
                    DepartmanAd = b.Departman.Ad,
                    PersonellerCount = b.Personeller.Count()
                }).FirstOrDefaultAsync();

            if(unit is null)
            {
                return BaseResult<ResultUnitDto>.Fail("Unit Not Found");
            }

            return BaseResult<ResultUnitDto>.Success(unit);



        }

        public async Task<BaseResult<object>> UpdateAsync(UpdateUnitDto updateDto)
        {
            var item = updateDto.Adapt<Birim>();

            var validationResult = await _updateValidator.ValidateAsync(updateDto);

            if (!validationResult.IsValid)
            {
                return BaseResult<object>.Fail(validationResult.Errors);

            }

            _unitReposirory.Update(item);

            var result = await _unitOfWork.SaveChangesAsync();

            return result ? BaseResult<object>.Success() : BaseResult<object>.Fail("Updated Failed");
        }
    }
}
