using HumanResources.Business.DTOs.ItemDtos;
using HumanResources.Business.Services.ItemServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HumanResources.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController(IItemService _itemService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _itemService.GetAllAsync();
            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _itemService.GetByIdAsync(id);
            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateItemDto item)
        {
            var response = await _itemService.CreateAsync(item);
            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _itemService.DeleteAsync(id);
            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateItemDto item)
        {
            var response = await _itemService.UpdateAsync(item);
            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }

        // --- Özel Metotlar ---

        [HttpGet("GetAllWithDetails")]
        public async Task<IActionResult> GetAllWithDetails()
        {
            // Admin Paneli: Tüm zimmetleri, kime ait olduğu ve türüyle birlikte listeler
            var response = await _itemService.GetAllItemsWithDetailsAsync();
            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }

        [HttpGet("GetByUserId/{userId}")]
        public async Task<IActionResult> GetByUserId(int userId)
        {
            // Profil Sayfası: İlgili personelin üzerindeki zimmetleri getirir
            var response = await _itemService.GetItemsByUserIdAsync(userId);
            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }

        [HttpGet("GetWithDetails/{id}")]
        public async Task<IActionResult> GetWithDetails(int id)
        {
            // Detay Sayfası: Tek bir eşyaya tıklanınca tüm özelliklerini ve kimde olduğunu getirir
            var response = await _itemService.GetItemWithDetailsByIdAsync(id);
            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }
    }
}