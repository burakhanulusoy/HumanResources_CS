using HumanResources.WebUI.DTOs.RoleDtos;
using HumanResources.WebUI.Services.RoleServices;
using HumanResources.WebUI.Services.UserServices;
using Microsoft.AspNetCore.Mvc;

namespace HumanResouerces.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RoleController(IRoleService _roleService, IUserService _userService) : Controller
    {
        // Rol listesi
        public async Task<IActionResult> Index()
        {
            var response = await _roleService.GetAllAsync();
            return View(response.Data);
        }

        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleDto createDto)
        {
            await _roleService.CreateAsync(createDto);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> UpdateRole(int id)
        {
            var response = await _roleService.GetByIdAsync(id); // BaseResult<UpdateRoleDto>
            return View(response.Data);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateRole(UpdateRoleDto updateDto)
        {
            await _roleService.UpdateAsync(updateDto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteRole(int id)
        {
            await _roleService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> RoleUsers(string roleName)
        {
            var response = await _userService.GetUsersByRoleAsync(roleName);
            ViewData["RoleName"] = roleName;
            return View(response.Data);
        }

        [HttpGet]
        public async Task<IActionResult> AssignRole(int id)
        {
            var response = await _roleService.GetUserForRoleAssignAsync(id);
            return View(response.Data);
        }

        [HttpPost]
        public async Task<IActionResult> AssignRole(List<AssignRoleDto> assignRoleDtos)
        {
            await _roleService.AssignRoleAsync(assignRoleDtos);
            return RedirectToAction(nameof(Index), "User");
        }
    }
}
