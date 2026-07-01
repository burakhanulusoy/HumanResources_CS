using HumanResources.Business.DTOs.ItemTypeDtos;
using HumanResources.Business.Services.ItemTypeServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HumanResources.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemTypesController(IItemTypeService _itemTypeService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _itemTypeService.GetAllAsync();
            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _itemTypeService.GetByIdAsync(id);
            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateItemTypeDto item)
        {
            var response = await _itemTypeService.CreateAsync(item);
            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _itemTypeService.DeleteAsync(id);
            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateItemTypeDto item)
        {
            var response = await _itemTypeService.UpdateAsync(item);
            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }

        // --- Özel Metotlar ---

        [HttpGet("GetAllWithItems")]
        public async Task<IActionResult> GetAllWithItems()
        {
            // Tüm zimmet türlerini ve o türden teslim edilen fiziksel eşyaların detaylarını listeler
            var response = await _itemTypeService.GetAllItemTypesWithItemsAsync();
            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }

        [HttpGet("GetWithItems/{id}")]
        public async Task<IActionResult> GetWithItems(int id)
        {
            // Tek bir zimmet türüne (Örn: Laptop) tıklanınca şirketteki tüm laptopları ve kimde olduklarını getirir
            var response = await _itemTypeService.GetItemTypeWithItemsByIdAsync(id);
            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }
    }
}