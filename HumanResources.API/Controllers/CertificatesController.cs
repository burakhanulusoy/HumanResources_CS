using HumanResources.Business.DTOs.CertificateDtos;
using HumanResources.Business.Services.CertificateServices;
using Microsoft.AspNetCore.Mvc;

namespace HumanResources.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CertificatesController(ICertificateService _certificateService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _certificateService.GetAllAsync();
            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _certificateService.GetByIdAsync(id);
            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateCertificateDto item)
        {
            var response = await _certificateService.CreateAsync(item);
            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _certificateService.DeleteAsync(id);
            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromForm] UpdateCertificateDto item)
        {
            var response = await _certificateService.UpdateAsync(item);
            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }





        [HttpGet("GetByUserId/{userId}")]
        public async Task<IActionResult> GetByUserId(int userId)
        {
            // Profil Sayfası: İlgili personelin tüm sertifikalarını getirir
            var response = await _certificateService.GetCertificateByUserIdAsync(userId);
            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }

        [HttpGet("GetUpcomingSoon/{days}")]
        public async Task<IActionResult> GetUpcomingSoon(int days)
        {
            // Bildirim/Dashboard: Belirtilen gün sayısı (örn: 30) içinde süresi dolacak belgeleri getirir
            var response = await _certificateService.GetDateUpcamingSoonAsync(days);
            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }

        [HttpGet("GetByCertificateTypeId/{typeId}")]
        public async Task<IActionResult> GetByCertificateTypeId(int typeId)
        {
            // Spesifik bir belge türüne sahip olan tüm personellerin belgelerini getirir
            var response = await _certificateService.GetUsersByCertificateTypeIdAsync(typeId);
            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }

        [HttpGet("GetAllWithInfo")]
        public async Task<IActionResult> GetAllWithInfo()
        {
            var response = await _certificateService.GetAllWithInfoAsync();
            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }

        [HttpGet("GetWithInfo/{id}")]
        public async Task<IActionResult> GetWithInfo(int id)
        {
            var response = await _certificateService.GetByIdWithInfoAsync(id);
            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }

    }
}