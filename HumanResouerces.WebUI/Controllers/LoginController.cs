using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using HumanResources.Business.DTOs.UserDtos;
using HumanResources.WebUI.Services.UserServices;
using Microsoft.AspNetCore.Mvc;

namespace HumanResouerces.WebUI.Controllers
{
    public class LoginController(IUserService _userService) : Controller
    {
        public IActionResult SignIn()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                return RedirectToDashboardBasedOnRole();
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(LoginUserDto loginUserDto)
        {
            // Servise gidip login kontrolünü yapıyoruz. Hata varsa Exception Filter yakalayıp View'a fırlatacak.
            var response = await _userService.LoginUserAsync(loginUserDto);

            if (response.IsSuccessful && response.Data != null)
            {
                var user = response.Data;

                // 1. Kimlik (Claims) Oluşturma İşlemi
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.UserName ?? ""),
                    new Claim(ClaimTypes.Email, user.Email ?? ""),
                    new Claim("FullName", $"{user.Ad} {user.Soyad}"),
                    new Claim("PhotoUrl", user.FotografUrl ?? "/assets/images/admin.png")
                };

                // 2. Kullanıcının rollerini sisteme tanıtma
                if (user.Roller != null)
                {
                    foreach (var role in user.Roller)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role));
                    }
                }

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true // Beni hatırla özelliği için
                };

                // 3. Güvenli Cookie ile sisteme giriş
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties
                );

                // 4. Role göre doğru sayfaya yönlendirme
                return RedirectToDashboardBasedOnRole(user.Roller);
            }

            return View(loginUserDto);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("SignIn", "Login");
        }

        private IActionResult RedirectToDashboardBasedOnRole(IList<string>? roles = null)
        {
            // Eğer giriş yapmış kullanıcının rollerini Controller içinden bulmak gerekiyorsa User.IsInRole kullanılır
            bool isAdmin = roles != null ? roles.Contains("Admin") : User.IsInRole("Admin");
            bool isAmir = roles != null ? roles.Contains("Amir") : User.IsInRole("Amir");

            if (isAdmin)
                return RedirectToAction("Index", "Dashboard", new { area = "Admin" });

            if (isAmir)
                return RedirectToAction("Index", "Dashboard", new { area = "Amir" });

            // Admin veya Amir değilse standart personel paneline gider
            return RedirectToAction("Index", "Dashboard", new { area = "Personel" });
        }
    }
}