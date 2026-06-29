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

        [HttpGet("id")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _userService.GetByIdAsync(id);
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


    }
}
