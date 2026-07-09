using HumanResouerces.WebUI.Areas.Admin.Models;
using HumanResources.WebUI.DTOs.UserDtos;
using HumanResources.WebUI.Services.DepartmentServices;
using HumanResources.WebUI.Services.ItemServices;
using HumanResources.WebUI.Services.ShiftServices;
using HumanResources.WebUI.Services.UnitServices;
using HumanResources.WebUI.Services.UserEducationServices;
using HumanResources.WebUI.Services.UserServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HumanResouerces.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController(IUserService _userService,
                                IUserEducationService _userEducationService,
                                IItemService _itemService,
                                IDepartmentService _departmentService,
                                IUnitService _unitService,
                                IShiftService _shiftService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var response = await _userService.GetAllUsersWithRolesAsync();
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

        public async Task<IActionResult> UserItems(int id)
        {
            var user = await _userService.GetByIdWithDepartmentAndUnitAsync(id);
            var items = await _itemService.GetItemsByUserIdAsync(id);
            var vm = new UserItemsViewModel
            {
                Kullanici = user.Data,
                Zimmetler = items.Data ?? new()
            };
            return View(vm);
        }

        public async Task<IActionResult> UserList()
        {
            var response = await _userService.GetAllWithDepartmentAndUnitAsync();
            return View(response.Data);
        }

        // ==================== PERSONEL CRUD ====================

        public async Task<IActionResult> CreateUser()
        {
            await FillDropdownsAsync();
            return View(new CreateUserDto());
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserDto createDto)
        {
            // Filter, ApiValidationException fırlarsa aynı view'ı ("CreateUser") tekrar render eder.
            // O yüzden dropdown'ları işlemden ÖNCE dolduruyoruz ki hata durumunda da ViewBag boş kalmasın.
            await FillDropdownsAsync();
            await _userService.CreateAsync(createDto);
            return RedirectToAction(nameof(UserList), new { basarili = "olusturuldu" });
        }

        public async Task<IActionResult> UpdateUser(int id)
        {
            var response = await _userService.GetByIdAsync(id);
            await FillDropdownsAsync();
            return View(response.Data);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUser(UpdateUserDto updateDto)
        {
            await FillDropdownsAsync();
            await _userService.UpdateAsync(updateDto);
            return RedirectToAction(nameof(UserList), new { basarili = "guncellendi" });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _userService.DeleteAsync(id);
            return RedirectToAction(nameof(UserList), new { basarili = "silindi" });
        }

        // ==================== YARDIMCI ====================

        private async Task FillDropdownsAsync()
        {
            var departmanlar = await _departmentService.GetAllAsync();
            var birimler = await _unitService.GetAllAsync();
            var amirler = await _userService.GetUsersByRoleAsync("Amir");   // ← GetAllAsync yerine bu
            var vardiyalar = await _shiftService.GetAllAsync();

            ViewBag.Departmanlar = departmanlar.Data ?? new();
            ViewBag.Birimler = birimler.Data ?? new();
            ViewBag.Amirler = amirler.Data ?? new();
            ViewBag.Vardiyalar = vardiyalar.Data ?? new();
        }


    }
}