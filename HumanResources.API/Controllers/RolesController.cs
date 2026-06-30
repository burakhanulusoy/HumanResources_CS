using HumanResources.Business.DTOs.DepartmentDtos;
using HumanResources.Business.DTOs.RoleDtos;
using HumanResources.Business.Services.RoleServices;
using Microsoft.AspNetCore.Mvc;

namespace HumanResources.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController(IRoleService _roleService) : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _roleService.GetAllAsync();
            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _roleService.GetByIdAsync(id);
            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRoleDto item)
        {
            var response = await _roleService.CreateAsync(item);
            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateRoleDto item)
        {
            var response = await _roleService.UpdateAsync(item);
            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _roleService.DeleteAsync(id);
            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }


        [HttpGet("getUserRole/{id}")]
        public async Task<IActionResult> GetUserForAssignRole(int id)
        {
            var response = await _roleService.GetUserForRoleAssignAsync(id);
            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }

        // Rota: POST api/roles/assign
        [HttpPost("assign")]
        public async Task<IActionResult> AssignRole(List<AssignRoleDto> assignRoleDtos)
        {
            var response = await _roleService.AssignRoleAsync(assignRoleDtos);
            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }



    }
}
