using HumanResources.Business.DTOs.UserDtos;
using Microsoft.AspNetCore.Mvc;

namespace HumanResouerces.WebUI.Controllers
{
    public class LoginController : Controller
    {
        
        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SignIn(LoginUserDto loginUserDto)
        {
            return View();
        }




    }
}
