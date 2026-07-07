using HumanResouerces.WebUI.Areas.Admin.Models;
using HumanResources.WebUI.DTOs.EducationDtos;
using HumanResources.WebUI.Services.EducationServices;
using HumanResources.WebUI.Services.UserServices;
using Microsoft.AspNetCore.Mvc;

namespace HumanResouerces.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EducationController(IEducationService _educationService,
                                     IUserService _userService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var response = await _educationService.GetAllWithUsersAsync();
            return View(response.Data);
        }

        public async Task<IActionResult> DetailsEducation(int id)
        {
            var response = await _educationService.GetWithUsersAsync(id);
            return View(response.Data);
        }

        public async Task<IActionResult> CreateEducation()
        {
            var users = await _userService.GetAllAsync();
            var vm = new EducationCreateViewModel
            {
                TumKullanicilar = users.Data ?? new()   // null gelirse boş liste
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEducation(EducationCreateViewModel vm)
        {
            await _educationService.CreateWithParticipantsAsync(vm.Egitim);
            return RedirectToAction(nameof(Index), new { basarili = "olusturuldu" });
        }

        public async Task<IActionResult> UpdateEducation(int id)
        {
            var response = await _educationService.GetByIdAsync(id);
            return View(response.Data);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateEducation(UpdateEducationDto updateDto)
        {
            await _educationService.UpdateAsync(updateDto);
            return RedirectToAction(nameof(Index), new { basarili = "guncellendi" });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteEducation(int id)
        {
            await _educationService.DeleteAsync(id);
            return RedirectToAction(nameof(Index), new { basarili = "silindi" });
        }
    }
}