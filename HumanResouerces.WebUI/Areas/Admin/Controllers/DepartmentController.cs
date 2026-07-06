using HumanResources.Business.DTOs.DepartmentDtos;
using HumanResources.WebUI.Services.DepartmentServices;
using Microsoft.AspNetCore.Mvc;

namespace HumanResouerces.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DepartmentController(IDepartmentService _departmentService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var response = await _departmentService.GetAllAsync();
            return View(response.Data);
        }

        public IActionResult CreateDepartment()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateDepartment(CreateDepartmentDto createDto)
        {
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
            var response = await _departmentService.GetByIdAsync(id);
            return View(response.Data);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateDepartment(UpdateDepartmentDto updateDto)
        {
            await _departmentService.UpdateAsync(updateDto);
            return RedirectToAction(nameof(Index));
        }

        // Departman inceleme ve "kimler var" kısmı
        public async Task<IActionResult> DepartmentDetails(int id)
        {
            var response = await _departmentService.GetDepartmentWithUserAsync(id);
            return View(response.Data);
        }
    }
}