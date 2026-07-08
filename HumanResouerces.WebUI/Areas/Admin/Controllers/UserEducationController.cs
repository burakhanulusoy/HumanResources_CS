using HumanResouerces.WebUI.Enums;
using HumanResources.WebUI.DTOs.UserEducationDtos;
using HumanResources.WebUI.Services.UserEducationServices;
using Microsoft.AspNetCore.Mvc;

namespace HumanResouerces.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserEducationController(IUserEducationService _userEducationService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var response = await _userEducationService.GetByStatusAsync(ApplicationStatus.Bekliyor);
            return View(response.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Approve(int id)
        {
            var current = await _userEducationService.GetByIdAsync(id);
            current.Data.BasvuruDurumu = ApplicationStatus.Onaylandi;
            await _userEducationService.UpdateAsync(current.Data);
            return RedirectToAction(nameof(Index), new { basarili = "onaylandi" });
        }

        [HttpPost]
        public async Task<IActionResult> Reject(int id, string? aciklama)
        {
            var current = await _userEducationService.GetByIdAsync(id);
            current.Data.BasvuruDurumu = ApplicationStatus.Reddedildi;
            current.Data.AdminAciklamasi = aciklama;
            await _userEducationService.UpdateAsync(current.Data);
            return RedirectToAction(nameof(Index), new { basarili = "reddedildi" });
        }

        // YENİ: Herhangi bir ekrandan (Education Details dahil) durum değiştirme
        // returnUrl sayesinde işlem sonrası çağrıldığı sayfaya geri döner
        [HttpPost]
        public async Task<IActionResult> ChangeStatus(int id, ApplicationStatus status, string? aciklama, string? returnUrl)
        {
            var current = await _userEducationService.GetByIdAsync(id);
            current.Data.BasvuruDurumu = status;
            current.Data.AdminAciklamasi = aciklama;

            await _userEducationService.UpdateAsync(current.Data);

            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect($"{returnUrl}?basarili=guncellendi");
            }

            return RedirectToAction(nameof(Index), new { basarili = "guncellendi" });
        }

        [HttpPost]
        public async Task<IActionResult> AddParticipant(int egitimId, int userId, string? returnUrl)
        {
            await _userEducationService.AddParticipantAsync(new CreateUserEducationDto
            {
                AppUserId = userId,
                EgitimId = egitimId
            });

            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                return Redirect($"{returnUrl}?basarili=eklendi");

            return RedirectToAction(nameof(Index), new { basarili = "eklendi" });
        }


    }
}