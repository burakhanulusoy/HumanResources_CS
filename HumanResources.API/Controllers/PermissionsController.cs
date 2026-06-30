using HumanResources.Business.DTOs.PermissionDtos;
using HumanResources.Business.Services.PermissionServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HumanResources.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionsController(IPermissionService _permissionService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _permissionService.GetAllAsync();
            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _permissionService.GetByIdAsync(id);
            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePermissionDto item)
        {
            var response = await _permissionService.CreateAsync(item);
            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _permissionService.DeleteAsync(id);
            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdatePermissionDto item)
        {
            var response = await _permissionService.UpdateAsync(item);
            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }


        [HttpGet("GetAllWithUser")]
        public async Task<IActionResult> GetAllWithUser()
        {
            var response = await _permissionService.GetAllPermissionWithUser();
            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }

        [HttpGet("GetWithUser/{id}")]
        public async Task<IActionResult> GetWithUser(int id)
        {
            var response = await _permissionService.GetPermissionWithUser(id);
            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }
    }
}