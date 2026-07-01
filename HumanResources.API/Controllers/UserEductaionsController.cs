using HumanResources.Business.DTOs.UserEducationDtos;
using HumanResources.Business.Services.UserEducationServices;
using HumanResources.Entity.Enums;
using Microsoft.AspNetCore.Mvc;

namespace HumanResources.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserEducationsController(IUserEducationService _userEducationService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _userEducationService.GetAllAsync();
            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _userEducationService.GetByIdAsync(id);
            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserEducationDto item)
        {
            var response = await _userEducationService.CreateAsync(item);
            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _userEducationService.DeleteAsync(id);
            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateUserEducationDto item)
        {
            var response = await _userEducationService.UpdateAsync(item);
            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }


        [HttpGet("GetByStatus/{durum}")]
        public async Task<IActionResult> GetByStatus(ApplicationStatus durum)
        {
            // Örn: Bekleyen (1) başvuruları getirmek için
            var response = await _userEducationService.GetApplicationStatusAsync(durum);
            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }

        [HttpGet("GetByUserId/{userId}")]
        public async Task<IActionResult> GetByUserId(int userId)
        {
            // Personelin profil sayfası: Aldığı/Başvurduğu eğitimler
            var response = await _userEducationService.GetEducationByUserIdAsync(userId);
            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }

        [HttpGet("GetByEducationId/{egitimId}")]
        public async Task<IActionResult> GetByEducationId(int egitimId)
        {
            // Admin paneli: Spesifik bir eğitime başvuran personeller
            var response = await _userEducationService.GetUsersByEducationIdAsync(egitimId);
            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }

        [HttpGet("GetAllWithInfo")]
        public async Task<IActionResult> GetAllWithInfo()
        {
            // Tüm başvuruları hem kullanıcı hem eğitim detaylarıyla birlikte getir
            var response = await _userEducationService.GetUserEducationWithAllInfoAsync();
            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }
    }
}