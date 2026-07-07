using HumanResources.WebUI.Services.UserServices;
using Microsoft.AspNetCore.Mvc;

namespace HumanResouerces.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController(IUserService _userService) : Controller
    {
        // Rol İşlemleri ekranı: tüm personel + rolleri
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

        // İŞTE SIFIR MANTIK, SIFIR İF! SADECE KÖPRÜ.
        public async Task<IActionResult> Details(int id)
        {
            var response = await _userService.GetByIdWithDepartmentAndUnitAsync(id);
            return View(response.Data);
        }
    }
}
