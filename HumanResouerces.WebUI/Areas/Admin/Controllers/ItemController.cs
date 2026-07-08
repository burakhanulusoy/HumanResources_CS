using HumanResources.WebUI.DTOs.ItemDtos;
using HumanResources.WebUI.Services.ItemServices;
using HumanResources.WebUI.Services.ItemTypeServices;
using HumanResources.WebUI.Services.UserServices;
using Microsoft.AspNetCore.Mvc;

namespace HumanResouerces.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ItemController(
        IItemService _itemService,
        IUserService _userService,
        IItemTypeService _itemTypeService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            // Listede isimleri ve türleri görmek için Details metodunu çağırıyoruz
            var response = await _itemService.GetAllItemsWithDetailsAsync();
            return View(response.Data);
        }

        public async Task<IActionResult> ItemDetails(int id)
        {
            // Eşya detayını ve kime zimmetli olduğunu getirir
            var response = await _itemService.GetItemWithDetailsByIdAsync(id);
            return View(response.Data);
        }

        public async Task<IActionResult> CreateItem()
        {
            await FillDropdownsAsync();
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
            await FillDropdownsAsync();
            var response = await _itemService.GetByIdAsync(id);
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

        // Dropdown'ları dolduran yardımcı metot
        private async Task FillDropdownsAsync()
        {
            var usersResponse = await _userService.GetAllAsync();
            var itemTypesResponse = await _itemTypeService.GetAllAsync();

            ViewBag.Users = usersResponse.Data;
            ViewBag.ItemTypes = itemTypesResponse.Data;
        }
    }
}