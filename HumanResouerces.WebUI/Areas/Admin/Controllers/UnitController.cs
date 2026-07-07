// =====================================================================
// UnitController güncellemesi — Create/Update formlarındaki departman
// dropdown'ı için departman listesini ViewBag ile view'e taşıyoruz.
// IDepartmentService'in WebUI'da kayıtlı olduğunu varsayıyorum.
// =====================================================================
using HumanResources.WebUI.DTOs.UnitDtos;
using HumanResources.WebUI.Services.DepartmentServices;
using HumanResources.WebUI.Services.UnitServices;
using Microsoft.AspNetCore.Mvc;

namespace HumanResouerces.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UnitController(IUnitService _unitService
                                , IDepartmentService _departmentService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var response = await _unitService.GetAllAsync();
            return View(response.Data);
        }

        public async Task<IActionResult> CreateUnit()
        {
            await LoadDepartmentsAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUnit(CreateUnitDto createDto)
        {
            await _unitService.CreateAsync(createDto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteUnit(int id)
        {
            await _unitService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> UpdateUnit(int id)
        {
            await LoadDepartmentsAsync();
            var response = await _unitService.GetByIdAsync(id);
            return View(response.Data);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUnit(UpdateUnitDto updateDto)
        {
            await _unitService.UpdateAsync(updateDto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> UnitUsers(int id)
        {
            var response = await _unitService.GetUnitWithUsersAsync(id);
            return View(response.Data);
        }

        private async Task LoadDepartmentsAsync()
        {
            var departments = await _departmentService.GetAllAsync();
            ViewBag.Departmanlar = departments.Data;
        }
    }
}