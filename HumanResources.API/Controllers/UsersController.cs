using HumanResources.Business.DTOs.DepartmentDtos;
using HumanResources.Business.DTOs.UserDtos;
using HumanResources.Business.Services.UserServices;
using Microsoft.AspNetCore.Mvc;

namespace HumanResources.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(IUserService _userService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _userService.GetAllAsync();

            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _userService.GetByIdAsync(id);
            return response.IsSuccessful ? Ok(response) : BadRequest(response);

        }


        [HttpGet("WithDepartmentAndUnit")]
        public async Task<IActionResult> GetAllWithDepartmentAndUnit()
        {
            var response = await _userService.GetAllUserWithDepartmentAndUnitAsync();

            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }

        [HttpGet("ByIdWithDepartmentAndUnit")]
        public async Task<IActionResult> GetByIdWithDepaertmentAndUnit(int id)
        {
            var response = await _userService.GetUserWithDepartmentAndUnitAsync(id);

            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }



        [HttpPost]
        public async Task<IActionResult> Create(CreateUserDto item)
        {

            var response = await _userService.CreateAsync(item);

            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            var response = await _userService.DeleteAsync(id);

            return response.IsSuccessful ? Ok(response) : BadRequest(response);

        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateUserDto item)
        {
            var response = await _userService.UpdateAsync(item);

            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }
        [HttpGet("GetSubordinates/{amirId}")]
        public async Task<IActionResult> GetSubordinates(int amirId)
        {
            // Amirin kendi ekibini görmesi VEYA İK'nın o amirin ekibini listelemesi için kullanılır
            var response = await _userService.GetSubordinatesAsync(amirId);
            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }
        [HttpGet("GetUsersByRole/{roleName}")]
        public async Task<IActionResult> GetUsersByRole(string roleName)
        {
            var response = await _userService.GetUsersByRoleAsync(roleName);
            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }

        // HumanResources.API.Controllers.UsersController içerisine eklenecek:
        [HttpGet("GetUsersByUnit/{unitId}")]
        public async Task<IActionResult> GetUsersByUnit(int unitId)
        {
            var response = await _userService.GetUsersByUnitIdAsync(unitId);
            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }
        [HttpGet("WithRoles")]
        public async Task<IActionResult> GetAllWithRoles()
        {
            var response = await _userService.GetAllUsersWithRolesAsync();
            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserDto loginUserDto)
        {
            var response = await _userService.LoginUserAsync(loginUserDto);
            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }


    }
}
