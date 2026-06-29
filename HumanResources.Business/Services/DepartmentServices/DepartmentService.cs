using FluentValidation;
using HumanResources.Business.Base;
using HumanResources.Business.DTOs.DepartmentDtos;
using HumanResources.DataAccess.Repositories.DepartmentRepositories;
using HumanResources.DataAccess.UOW;
using HumanResources.Entity.Entities;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace HumanResources.Business.Services.DepartmentServices
{
    public class DepartmentService(IDepartmentRepository _departmentRepository
                                   ,IUnitOfWork _unitOfWork
                                   ,IValidator<UpdateDepartmentDto> _updateValidator
                                   ,IValidator<CreateDepartmentDto> _createValidator) : IDepartmentService
    {
        public async Task<BaseResult<object>> CreateAsync(CreateDepartmentDto createDto)
        {
            var department = createDto.Adapt<Departman>();

            var validationResult = await _createValidator.ValidateAsync(createDto);

            if(!validationResult.IsValid)
            {
                return BaseResult<object>.Fail(validationResult.Errors); 

            }

            await _departmentRepository.CreateAsync(department);

            var result = await _unitOfWork.SaveChangesAsync();

            return result ? BaseResult<object>.Success(department) : BaseResult<object>.Fail("Created Failed");

        }

        public async Task<BaseResult<object>> DeleteAsync(int id)
        {
            
            var item = await _departmentRepository.GetByIdAsync(id);

            if(item is null)
            {
                return BaseResult<object>.Fail("Department Not Found");
            }

            _departmentRepository.Delete(item);

            var result = await _unitOfWork.SaveChangesAsync();

            return result ? BaseResult<object>.Success() : BaseResult<object>.Fail("Deleted Failed");

        }

        public async Task<BaseResult<List<ResultDepartmentDto>>> GetAllAsync()
        {
            var mappedItems = await _departmentRepository.GetQueryable()
                .ProjectToType<ResultDepartmentDto>() // Mapster'ýn IQueryable extension'ý
                .ToListAsync();                      

            return BaseResult<List<ResultDepartmentDto>>.Success(mappedItems);
        }

        public async Task<BaseResult<ResultDepartmentDto>> GetByIdAsync(int id)
        {
          
            var mappedItem = await _departmentRepository.GetQueryable()
                .Where(x => x.Id == id)
                .ProjectToType<ResultDepartmentDto>()
                .FirstOrDefaultAsync();

            if (mappedItem is null)
            {
                return BaseResult<ResultDepartmentDto>.Fail("Department Not Found");
            }

            return BaseResult<ResultDepartmentDto>.Success(mappedItem);
        }

        public async Task<BaseResult<object>> UpdateAsync(UpdateDepartmentDto updateDto)
        {
            var item = updateDto.Adapt<Departman>();

            var validationResult = await _updateValidator.ValidateAsync(updateDto);

            if (!validationResult.IsValid)
            {
                return BaseResult<object>.Fail(validationResult.Errors);

            }

             _departmentRepository.Update(item);

            var result = await _unitOfWork.SaveChangesAsync();

            return result ? BaseResult<object>.Success() : BaseResult<object>.Fail("Updated Failed");


        }
    }
}
