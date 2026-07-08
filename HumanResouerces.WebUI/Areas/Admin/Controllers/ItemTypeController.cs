using HumanResources.WebUI.DTOs.ItemTypeDtos;
using HumanResources.WebUI.Services.ItemTypeServices;
using Microsoft.AspNetCore.Mvc;

namespace HumanResouerces.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ItemTypeController(IItemTypeService _itemTypeService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var response = await _itemTypeService.GetAllAsync();
            return View(response.Data);
        }

        public IActionResult CreateItemType()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateItemType(CreateItemTypeDto createDto)
        {
            await _itemTypeService.CreateAsync(createDto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteItemType(int id)
        {
            await _itemTypeService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> UpdateItemType(int id)
        {
            var response = await _itemTypeService.GetByIdAsync(id);
            return View(response.Data);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateItemType(UpdateItemTypeDto updateDto)
        {
            await _itemTypeService.UpdateAsync(updateDto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> TypeItems(int id)
        {
            var response = await _itemTypeService.GetWithItemsAsync(id);
            return View(response.Data);
        }
    }
}