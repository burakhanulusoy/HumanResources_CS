using HumanResources.Business.DTOs.UnitDtos;
using HumanResources.Business.Services.UnitServices;
using Microsoft.AspNetCore.Mvc;

namespace HumanResources.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitsController(IUnitService _unitService) : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _unitService.GetAllAsync();

            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _unitService.GetByIdAsync(id);
            return response.IsSuccessful ? Ok(response) : BadRequest(response);

        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUnitDto item)
        {

            var response = await _unitService.CreateAsync(item);

            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            var response = await _unitService.DeleteAsync(id);

            return response.IsSuccessful ? Ok(response) : BadRequest(response);

        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateUnitDto item)
        {
            var response = await _unitService.UpdateAsync(item);

            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }


        [HttpGet("GetUnitWithUsers/{id}")]
        public async Task<IActionResult> GetUnitWithUsers(int id)
        {
            var response = await _unitService.GetUnitWithUsersAsync(id);
            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }


    }
}
