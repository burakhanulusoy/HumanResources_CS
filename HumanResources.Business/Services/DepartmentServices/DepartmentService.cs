using FluentValidation;
using HumanResources.Business.Base;
using HumanResources.Business.DTOs.DepartmentDtos;
using HumanResources.DataAccess.Repositories.DepartmentRepositories;
using HumanResources.DataAccess.UOW;
using HumanResources.Entity.Entities;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace HumanResources.Business.Services.DepartmentServices
{
    public class DepartmentService(IDepartmentRepository _departmentRepository
                                   ,IUnitOfWork _unitOfWork
                                   ,IValidator<UpdateDepartmentDto> _updateValidator
                                   ,IValidator<CreateDepartmentDto> _createValidator
                                   ,UserManager<AppUser> _userManager) : IDepartmentService
    {
        public async Task<BaseResult<object>> CreateAsync(CreateDepartmentDto createDto)
        {
            var validationResult = await _createValidator.ValidateAsync(createDto);
            if (!validationResult.IsValid)
            {
                return BaseResult<object>.Fail(validationResult.Errors);
            }

            if (createDto.YoneticiId.HasValue)
            {
                var user = await _userManager.FindByIdAsync(createDto.YoneticiId.Value.ToString());

                if (user == null)
                {
                    return BaseResult<object>.Fail("Belirtilen yönetici sistemde bulunamadý.");
                }

                var isManager = await _userManager.IsInRoleAsync(user, "Yönetici");
                if (!isManager)
                {
                    return BaseResult<object>.Fail("Seçilen kiþi yönetici rolüne sahip deðil!");
                }
            }

            var department = createDto.Adapt<Departman>();
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

        public async Task<BaseResult<List<ResultDepartmentWithUserDto>>> GetDepartmentsWithUserAsync()
        {
            var items = await _departmentRepository.GetDepartmentsWithUserAsync();

            var mappedItem = items.Adapt<List<ResultDepartmentWithUserDto>>();

            return BaseResult<List<ResultDepartmentWithUserDto>>.Success(mappedItem);



        }

        public async Task<BaseResult<ResultDepartmentWithUserDto>> GetDepartmentWithUserAsync(int id)
        {

            var items = await _departmentRepository.GetDepartmentWithUserAsync(id);

            if(items is null)
            {
                return BaseResult<ResultDepartmentWithUserDto>.Fail("Department Not Found");
            }


            var mappedItem = items.Adapt<ResultDepartmentWithUserDto>();

            return BaseResult<ResultDepartmentWithUserDto>.Success(mappedItem);




        }

        public async Task<BaseResult<object>> UpdateAsync(UpdateDepartmentDto updateDto)
        {
            var validationResult = await _updateValidator.ValidateAsync(updateDto);
            if (!validationResult.IsValid)
            {
                return BaseResult<object>.Fail(validationResult.Errors);
            }

            var department = await _departmentRepository.GetByIdAsync(updateDto.Id); // Kendi repository'ndeki get metodunu yaz
            if (department == null)
            {
                return BaseResult<object>.Fail("Güncellenecek departman bulunamadý.");
            }

            if (updateDto.YoneticiId.HasValue) // içinde deðer var mý unutma daha sistemsel olmasý lazým async için 
            {
                var user = await _userManager.FindByIdAsync(updateDto.YoneticiId.Value.ToString());

                if (user == null)
                {
                    return BaseResult<object>.Fail("Belirtilen yönetici sistemde bulunamadý.");
                }

                var isManager = await _userManager.IsInRoleAsync(user, "Yönetici");
                if (!isManager)
                {
                    return BaseResult<object>.Fail("Seçilen kiþi yönetici rolüne sahip deðil!");
                }
            }

            updateDto.Adapt(department); 

            _departmentRepository.Update(department);

            var result = await _unitOfWork.SaveChangesAsync();

            return result ? BaseResult<object>.Success() : BaseResult<object>.Fail("Updated Failed");

        }
    }
}
