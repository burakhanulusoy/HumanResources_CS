using HumanResources.Business.DTOs.DepartmentDtos;
using HumanResources.Business.Services.DepartmentServices;
using Microsoft.AspNetCore.Mvc;

namespace HumanResources.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController(IDepartmentService _departmentService) : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _departmentService.GetAllAsync();

            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _departmentService.GetByIdAsync(id);
            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateDepartmentDto item)
        {
            var response = await _departmentService.CreateAsync(item);

            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _departmentService.DeleteAsync(id);

            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateDepartmentDto item)
        {
            var response = await _departmentService.UpdateAsync(item);

            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }

        [HttpGet("GetDepartmentsWithUser")]
        public async Task<IActionResult> GetAllWithUser()
        {
            var response = await _departmentService.GetDepartmentsWithUserAsync();

            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }

        // ROUTE GÜNCELLENDİ: {id} parametresi eklendi
        [HttpGet("GetDepartmentWithUser/{id}")]
        public async Task<IActionResult> GetWithUserById(int id)
        {
            var response = await _departmentService.GetDepartmentWithUserAsync(id);

            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }

        [HttpGet("GetDepartmentWithUnits/{id}")]
        public async Task<IActionResult> GetDepartmentWithUnits(int id)
        {
            var response = await _departmentService.GetDepartmentWithUnitsAsync(id);
            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }

    }
}