using HumanResources.WebUI.DTOs.DemirbasDtos;
using HumanResources.WebUI.Services.DemirbasServices;
using HumanResources.WebUI.Services.ItemTypeServices;
using Microsoft.AspNetCore.Mvc;

namespace HumanResouerces.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DemirbasController(
        IDemirbasService _demirbasService,
        IItemTypeService _itemTypeService,
        IConfiguration _configuration) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var response = await _demirbasService.GetAllWithTypeAsync();
            ViewBag.ApiBaseUrl = _configuration["ApiOptions:baseUrl"] + "/api/";
            return View(response.Data);
        }

        public async Task<IActionResult> CreateDemirbas()
        {
            await FillTypesAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateDemirbas(CreateDemirbasDto createDto)
        {
            await _demirbasService.CreateAsync(createDto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> UpdateDemirbas(int id)
        {
            await FillTypesAsync();
            var response = await _demirbasService.GetByIdAsync(id);
            return View(response.Data);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateDemirbas(UpdateDemirbasDto updateDto)
        {
            await _demirbasService.UpdateAsync(updateDto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteDemirbas(int id)
        {
            var result = await _demirbasService.DeleteAsync(id);

            if (result.IsSuccessful)
            {
                TempData["BasariMesaji"] = "Demirbaş başarıyla silindi.";
            }
            else
            {
                string mesaj = "Demirbaş silinemedi.";
                if (result.Errors != null && result.Errors.Any())
                {
                    mesaj = string.Join(" ", result.Errors.Select(e => e.ErrorMessage));
                }
                TempData["HataMesaji"] = mesaj;
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task FillTypesAsync()
        {
            var typesResponse = await _itemTypeService.GetAllAsync();
            ViewBag.ItemTypes = typesResponse.Data;
        }
    }
}