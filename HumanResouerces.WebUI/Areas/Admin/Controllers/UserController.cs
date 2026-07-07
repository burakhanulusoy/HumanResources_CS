using HumanResouerces.WebUI.Areas.Admin.Models;
using HumanResources.WebUI.Services.UserEducationServices;
using HumanResources.WebUI.Services.UserServices;
using Microsoft.AspNetCore.Mvc;

namespace HumanResouerces.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController(IUserService _userService,
                                IUserEducationService _userEducationService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var response = await _userService.GetAllUsersWithRolesAsync();
            return View(response.Data);
        }

        public async Task<IActionResult> UnitUsers(int id)
        {
            var response = await _userService.GetUsersByUnitIdAsync(id);
            return View(response.Data);
        }

        public async Task<IActionResult> Details(int id)
        {
            var response = await _userService.GetByIdWithDepartmentAndUnitAsync(id);
            return View(response.Data);
        }

        public async Task<IActionResult> EducationDetails(int id)
        {
            var user = await _userService.GetByIdWithDepartmentAndUnitAsync(id);
            var educations = await _userEducationService.GetByUserIdAsync(id);
            var vm = new UserDetailsViewModel
            {
                Kullanici = user.Data,
                Egitimler = educations.Data
            };
            return View(vm);
        }

        public async Task<IActionResult> UserList()
        {
            var response = await _userService.GetAllWithDepartmentAndUnitAsync();
            return View(response.Data);
        }
    }
}