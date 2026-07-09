using HumanResources.WebUI.DTOs.ItemDtos;
using HumanResources.WebUI.Services.DemirbasServices;
using HumanResources.WebUI.Services.ItemServices;
using HumanResources.WebUI.Services.UserServices;
using Microsoft.AspNetCore.Mvc;

namespace HumanResouerces.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ItemController(
        IItemService _itemService,
        IUserService _userService,
        IDemirbasService _demirbasService,
        IConfiguration _configuration) : Controller   // ItemType yerine Demirbas
    {
        public async Task<IActionResult> Index()
        {
            var response = await _itemService.GetAllItemsWithDetailsAsync();
            ViewBag.ApiBaseUrl = _configuration["ApiOptions:baseUrl"] + "/api/";
            return View(response.Data);
        }



        public async Task<IActionResult> ItemDetails(int id)
        {
            var response = await _itemService.GetItemWithDetailsByIdAsync(id);
            return View(response.Data);
        }

        public async Task<IActionResult> CreateItem()
        {
            await FillDropdownsAsync();   // sadece MÜSAİT demirbaşlar
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateItem(CreateItemDto createDto)
        {
            await _itemService.CreateAsync(createDto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> UpdateItem(int id)
        {
            var response = await _itemService.GetByIdAsync(id);

            // Güncellemede: mevcut demirbaş + hâlâ müsait olanlar dropdown'da görünsün
            await FillDropdownsAsync(response.Data?.DemirbasId);
            return View(response.Data);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateItem(UpdateItemDto updateDto)
        {
            await _itemService.UpdateAsync(updateDto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteItem(int id)
        {
            await _itemService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ZimmetBelgesi(int id)
        {
            var response = await _itemService.GetItemWithDetailsByIdAsync(id);
            return View(response.Data);
        }

        private async Task FillDropdownsAsync(int? currentDemirbasId = null)
        {
            var usersResponse = await _userService.GetAllAsync();
            var availableResponse = await _demirbasService.GetAvailableAsync();

            var list = availableResponse.Data ?? new List<HumanResources.WebUI.DTOs.DemirbasDtos.ResultDemirbasDto>();

            // Güncelleme senaryosu: seçili demirbaş "Zimmetli" olduğu için müsait listede gelmez.
            // Kullanıcının mevcut seçimini kaybetmemek için onu da ekliyoruz.
            if (currentDemirbasId.HasValue && currentDemirbasId.Value > 0
                && !list.Any(d => d.Id == currentDemirbasId.Value))
            {
                var currentResponse = await _demirbasService.GetByIdAsync(currentDemirbasId.Value);
                if (currentResponse?.Data != null)
                {
                    list.Add(new HumanResources.WebUI.DTOs.DemirbasDtos.ResultDemirbasDto
                    {
                        Id = currentResponse.Data.Id,
                        Marka = currentResponse.Data.Marka,
                        Model = currentResponse.Data.Model,
                        SeriNumarasi = currentResponse.Data.SeriNumarasi
                    });
                }
            }

            ViewBag.Users = usersResponse.Data;
            ViewBag.Demirbaslar = list;
        }
    }
}