using HumanResources.Business.DTOs.ShiftDtos;
using HumanResources.Business.Services.ShiftServices;
using Microsoft.AspNetCore.Mvc;

namespace HumanResources.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShiftsController(IShiftService _shiftService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _shiftService.GetAllAsync();

            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _shiftService.GetByIdAsync(id);

            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateShiftDto item)
        {
            var response = await _shiftService.CreateAsync(item);

            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateShiftDto item)
        {
            var response = await _shiftService.UpdateAsync(item);

            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _shiftService.DeleteAsync(id);

            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }
    }
}