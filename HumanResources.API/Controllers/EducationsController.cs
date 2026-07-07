// API/Controllers/EducationsController.cs
using HumanResources.Business.DTOs.EducationDtos;
using HumanResources.Business.Services.EducationServices;
using Microsoft.AspNetCore.Mvc;

namespace HumanResources.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EducationsController(IEducationService _educationService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _educationService.GetAllAsync();
            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _educationService.GetByIdAsync(id);
            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateEducationDto item)
        {
            var response = await _educationService.CreateAsync(item);
            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _educationService.DeleteAsync(id);
            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateEducationDto item)
        {
            var response = await _educationService.UpdateAsync(item);
            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }

        [HttpGet("GetAllWithUsers")]
        public async Task<IActionResult> GetAllWithUsers()
        {
            var response = await _educationService.GetAllEducationWithUsersAsync();
            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }

        [HttpGet("GetWithUsers/{id}")]
        public async Task<IActionResult> GetWithUsers(int id)
        {
            var response = await _educationService.GetEducationWithUsersAsync(id);
            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }

        [HttpPost("CreateWithParticipants")]
        public async Task<IActionResult> CreateWithParticipants(CreateEducationWithParticipantsDto item)
        {
            var response = await _educationService.CreateWithParticipantsAsync(item);
            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }
    }
}