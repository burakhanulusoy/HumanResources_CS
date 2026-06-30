using HumanResources.Business.Base;
using HumanResources.Business.DTOs.RoleDtos;
using HumanResources.Entity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HumanResources.Business.Services.RoleServices
{
    public class RoleService(RoleManager<AppRole> _roleManager,UserManager<AppUser> _userManager) : IRoleService
    {
        public async Task<BaseResult<object>> AssignRoleAsync(List<AssignRoleDto> assignRoleDtos)
        {
            var userId = assignRoleDtos.Select(x => x.UserId).FirstOrDefault();

            var user = await _userManager.FindByIdAsync(userId.ToString());

            foreach (var role in assignRoleDtos)
            {

                if (role.RoleExist)
                {
                    await _userManager.AddToRoleAsync(user, role.RoleName);
                }
                else
                {
                    await _userManager.RemoveFromRoleAsync(user, role.RoleName);
                }



            }


            return BaseResult<object>.Success(new { Message = "Assign Role Successful." });
        }

        public async Task<BaseResult<object>> CreateAsync(CreateRoleDto createDto)
        {
            var roleExists = await _roleManager.RoleExistsAsync(createDto.Name);
            if (roleExists)
            {
                return BaseResult<object>.Fail("Bu rol zaten mevcut.");
            }

            var role = new AppRole
            {
                Name = createDto.Name,
                SilindiMi = false
            };

            var result = await _roleManager.CreateAsync(role);

            if (!result.Succeeded)
            {
                return BaseResult<object>.Fail(result.Errors);
            }

            return BaseResult<object>.Success(new { Message = "Rol baþarýyla oluþturuldu." });
        }

        public async Task<BaseResult<object>> DeleteAsync(int id)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());

            if (role is null || role.SilindiMi)
            {
                return BaseResult<object>.Fail("Silinecek rol bulunamadý.");
            }

            role.SilindiMi = true;

            var result = await _roleManager.UpdateAsync(role);

            if (!result.Succeeded)
            {
                return BaseResult<object>.Fail(result.Errors);
            }

            return BaseResult<object>.Success(new { Message = "Rol baþarýyla silindi." });
        }

        public async Task<BaseResult<List<ResultRoleDto>>> GetAllAsync()
        {
            var roles = await _roleManager.Roles
                .Where(r => !r.SilindiMi)
                .AsNoTracking()
                .ToListAsync();

            var mappedRoles = roles.Select(r => new ResultRoleDto
            {
                Id = r.Id,
                Name = r.Name
            }).ToList();

            return BaseResult<List<ResultRoleDto>>.Success(mappedRoles);
        }

        public async Task<BaseResult<ResultRoleDto>> GetByIdAsync(int id)
        {
            var role = await _roleManager.Roles
                .FirstOrDefaultAsync(r => r.Id == id && !r.SilindiMi);

            if (role is null)
            {
                return BaseResult<ResultRoleDto>.Fail("Rol bulunamadý.");
            }

            var mappedRole = new ResultRoleDto
            {
                Id = role.Id,
                Name = role.Name
            };

            return BaseResult<ResultRoleDto>.Success(mappedRole);
        }

        public async Task<BaseResult<List<AssignRoleDto>>> GetUserForRoleAssignAsync(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            if (user is null)
            {
                return BaseResult<List<AssignRoleDto>>.Fail("User Not Found");
            }

            var roles = await _roleManager.Roles.ToListAsync();

            var userRoles = await _userManager.GetRolesAsync(user);

            var assignRoleList = new List<AssignRoleDto>();

            foreach (var role in roles)
            {

                assignRoleList.Add(new AssignRoleDto
                {
                    FullName = string.Join(" ", user.Ad, user.Soyad),
                    RoleId = role.Id,
                    UserId = user.Id,
                    RoleName = role.Name,
                    RoleExist = userRoles.Contains(role.Name) //true ya da false


                });


            }

            return BaseResult<List<AssignRoleDto>>.Success(assignRoleList);
        }

        public async Task<BaseResult<object>> UpdateAsync(UpdateRoleDto updateDto)
        {
            var role = await _roleManager.FindByIdAsync(updateDto.Id.ToString());

            if (role is null || role.SilindiMi)
            {
                return BaseResult<object>.Fail("Güncellenecek rol bulunamadý.");
            }

            role.Name = updateDto.Name;

            var result = await _roleManager.UpdateAsync(role);

            if (!result.Succeeded)
            {
                return BaseResult<object>.Fail(result.Errors);
            }

            return BaseResult<object>.Success(new { Message = "Rol baþarýyla güncellendi." });
        }
    }
}
