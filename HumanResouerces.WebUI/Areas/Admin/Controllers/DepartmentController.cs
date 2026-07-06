using HumanResources.Business.DTOs.DepartmentDtos;
using HumanResources.WebUI.Services.DepartmentServices;
using HumanResources.WebUI.Services.UserServices;
using Microsoft.AspNetCore.Mvc;

namespace HumanResouerces.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DepartmentController(IDepartmentService _departmentService, IUserService _userService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var response = await _departmentService.GetAllAsync();
            return View(response.Data);
        }

        public async Task<IActionResult> CreateDepartment()
        {
            await LoadManagersAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateDepartment(CreateDepartmentDto createDto)
        {
            await LoadManagersAsync();
            await _departmentService.CreateAsync(createDto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteDepartment(int id)
        {
            await _departmentService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> UpdateDepartment(int id)
        {
            await LoadManagersAsync();
            var response = await _departmentService.GetByIdAsync(id);
            return View(response.Data);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateDepartment(UpdateDepartmentDto updateDto)
        {
            await LoadManagersAsync();
            await _departmentService.UpdateAsync(updateDto);
            return RedirectToAction(nameof(Index));
        }

        // Departmanın birimlerini listeler
        public async Task<IActionResult> DepartmentUnits(int id)
        {
            var response = await _departmentService.GetDepartmentWithUnitsAsync(id);
            return View(response.Data);
        }

        // YENİ EKLENEN METOT: Departmanın personellerini ve detaylarını listeler (PDF Sayfası için)
        public async Task<IActionResult> DepartmentDetails(int id)
        {
            var response = await _departmentService.GetDepartmentWithUserAsync(id);
            return View(response.Data);
        }

        // Yönetici dropdown'ı: sadece "Yönetici" rolündeki kullanıcılar
        private async Task LoadManagersAsync()
        {
            var users = await _userService.GetUsersByRoleAsync("Yönetici");
            ViewBag.Users = users.Data;
        }
    }
}