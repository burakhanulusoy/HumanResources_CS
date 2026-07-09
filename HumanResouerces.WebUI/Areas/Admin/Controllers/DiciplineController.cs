using HumanResouerces.WebUI.Exceptions;
using HumanResouerces.WebUI.Models;
using HumanResources.WebUI.DTOs.DiciplineDtos;
using HumanResources.WebUI.Services.DiciplineServices;
using HumanResources.WebUI.Services.UserServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HumanResouerces.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DiciplineController(IDiciplineService _diciplineService, IUserService _userService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var response = await _diciplineService.GetAllAsync();
            return View(response.Data);
        }

        [HttpGet]
        public async Task<IActionResult> CreateDicipline()
        {
            var vm = new DiciplineCreateViewModel { Users = await GetUserSelectListAsync() };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateDicipline(DiciplineCreateViewModel vm)
        {
            try
            {
                await _diciplineService.CreateAsync(vm.Record);
                TempData["Success"] = "Kayıt başarıyla oluşturuldu.";
                return RedirectToAction(nameof(Index));
            }
            catch (ApiValidationException ex)
            {
                // API'den dönen validation hatalarını forma geri yansıt
                AddApiErrorsToModelState(ex);
                vm.Users = await GetUserSelectListAsync();
                return View(vm);
            }
        }

        [HttpGet]
        public async Task<IActionResult> UpdateDicipline(int id)
        {
            var response = await _diciplineService.GetByIdAsync(id);
            if (response?.Data is null)
            {
                TempData["Error"] = "Güncellenecek kayıt bulunamadı.";
                return RedirectToAction(nameof(Index));
            }

            var vm = new DiciplineUpdateViewModel
            {
                Record = response.Data,
                Users = await GetUserSelectListAsync()
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateDicipline(DiciplineUpdateViewModel vm)
        {
            try
            {
                await _diciplineService.UpdateAsync(vm.Record);
                TempData["Success"] = "Kayıt başarıyla güncellendi.";
                return RedirectToAction(nameof(Index));
            }
            catch (ApiValidationException ex)
            {
                AddApiErrorsToModelState(ex);
                vm.Users = await GetUserSelectListAsync();
                return View(vm);
            }
        }

        public async Task<IActionResult> DeleteDicipline(int id)
        {
            var response = await _diciplineService.DeleteAsync(id);
            TempData[response.IsSuccessful ? "Success" : "Error"] =
                response.IsSuccessful ? "Kayıt silindi." : "Silme işlemi başarısız oldu.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DiciplineDetails(int id, int userId)
        {
            var response = await _diciplineService.GetByUserIdAsync(userId);
            var allRecords = response?.Data ?? new List<DiciplineDto>();

            var currentRecord = allRecords.FirstOrDefault(x => x.Id == id);
            if (currentRecord is null)
            {
                TempData["Error"] = "Kayıt bulunamadı.";
                return RedirectToAction(nameof(Index));
            }

            var viewModel = new DiciplineDetailViewModel
            {
                CurrentRecord = currentRecord,
                OtherRecords = allRecords.Where(x => x.Id != id)
                                         .OrderByDescending(x => x.OlayTarihi)
                                         .ToList()
            };
            return View(viewModel);
        }

        // ---- Yardımcılar ----

        private async Task<List<SelectListItem>> GetUserSelectListAsync()
        {
            var usersResponse = await _userService.GetAllAsync();
            return usersResponse?.Data?
                .Select(u => new SelectListItem($"{u.Ad} {u.Soyad}", u.Id.ToString()))
                .ToList() ?? new List<SelectListItem>();
        }

        private void AddApiErrorsToModelState(ApiValidationException ex)
        {
            if (ex.Errors is not null && ex.Errors.Any())
            {
                foreach (var error in ex.Errors)
                {
                    // API'den gelen PropertyName "OlayTarihi" gibi geliyor,
                    // formdaki alan adı ise "Record.OlayTarihi" — eşleşmesi için önek ekliyoruz.
                    var key = string.IsNullOrEmpty(error.PropertyName) ? string.Empty : $"Record.{error.PropertyName}";
                    ModelState.AddModelError(key, error.ErrorMessage);
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "İşlem sırasında bir hata oluştu.");
            }
        }

        public async Task<IActionResult> Record(int id, int userId)
        {
            var response = await _diciplineService.GetByUserIdAsync(userId);
            var record = response?.Data?.FirstOrDefault(x => x.Id == id);

            if (record is null)
            {
                TempData["Error"] = "Kayıt bulunamadı.";
                return RedirectToAction(nameof(Index));
            }

            return View(record);
        }

    }
}
