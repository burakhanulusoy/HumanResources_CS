using HumanResources.Business.DTOs.PermissionTypeDtos;
using HumanResources.Business.Services.PermissionTypeServices;
using Microsoft.AspNetCore.Mvc;

namespace HumanResources.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionTypesController(IPermissionTypeService _permissionTypeService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _permissionTypeService.GetAllAsync();
            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _permissionTypeService.GetByIdAsync(id);
            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePermissionTypeDto item)
        {
            var response = await _permissionTypeService.CreateAsync(item);
            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _permissionTypeService.DeleteAsync(id);
            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdatePermissionTypeDto item)
        {
            var response = await _permissionTypeService.UpdateAsync(item);
            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }

        // --- Özel Metotlar ---

        [HttpGet("GetAllWithPermissions")]
        public async Task<IActionResult> GetAllWithPermissions()
        {
            var response = await _permissionTypeService.GetAllPermissionTypeWithPermissions();
            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }

        [HttpGet("GetWithPermissions/{id}")]
        public async Task<IActionResult> GetWithPermissions(int id)
        {
            var response = await _permissionTypeService.GetPermissionTypeWithPermissions(id);
            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }
    }
}