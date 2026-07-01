using HumanResources.Business.DTOs.CertificateTypeDtos;
using HumanResources.Business.Services.CertificateTypeServices;
using Microsoft.AspNetCore.Mvc;

namespace HumanResources.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CertificateTypesController(ICertificateTypeService _certificateTypeService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _certificateTypeService.GetAllAsync();
            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _certificateTypeService.GetByIdAsync(id);
            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCertificateTypeDto item)
        {
            var response = await _certificateTypeService.CreateAsync(item);
            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _certificateTypeService.DeleteAsync(id);
            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateCertificateTypeDto item)
        {
            var response = await _certificateTypeService.UpdateAsync(item);
            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }


        [HttpGet("GetAllWithCertificates")]
        public async Task<IActionResult> GetAllWithCertificates()
        {
            // Tüm türleri ve o türden belge alan personelleri getir
            var response = await _certificateTypeService.GetAllCertificateTypeWithCertificateAsync();
            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }

        [HttpGet("GetWithCertificates/{id}")]
        public async Task<IActionResult> GetWithCertificates(int id)
        {
            // Sadece bir türe (Örn: Forklift) ait belge alan personelleri getir
            var response = await _certificateTypeService.GetCertificateTypeWithCertificateAsync(id);
            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }
    }
}