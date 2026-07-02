using HumanResources.Business.DTOs.DiciplineDtos;
using HumanResources.Business.Services.DiciplineServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HumanResources.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiciplinesController(IDiciplineService _diciplineService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _diciplineService.GetAllAsync();
            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _diciplineService.GetByIdAsync(id);
            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateDiciplineDto item)
        {
            var response = await _diciplineService.CreateAsync(item);
            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _diciplineService.DeleteAsync(id);
            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromForm] UpdateDiciplineDto item)
        {
            var response = await _diciplineService.UpdateAsync(item);
            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }

        [HttpGet("GetByUserId/{userId}")]
        public async Task<IActionResult> GetByUserId(int userId)
        {
            var response = await _diciplineService.GetByUserIdAsync(userId);
            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }
    }
}