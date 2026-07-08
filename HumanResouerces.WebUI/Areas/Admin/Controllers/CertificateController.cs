using HumanResouerces.WebUI.Areas.Admin.Models;
using HumanResources.WebUI.DTOs.CertificateDtos;
using HumanResources.WebUI.Services.CertificateServices;
using HumanResources.WebUI.Services.CertificateTypeServices;
using HumanResources.WebUI.Services.UserServices;
using Microsoft.AspNetCore.Mvc;

namespace HumanResouerces.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CertificateController(ICertificateService _certificateService,
                                       ICertificateTypeService _certificateTypeService,
                                       IUserService _userService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var response = await _certificateService.GetAllWithInfoAsync();
            return View(response.Data);
        }

        // İNCELE: sertifika + personel (departman/birim) + tür bilgisi
        public async Task<IActionResult> DetailsCertificate(int id)
        {
            var response = await _certificateService.GetWithInfoAsync(id);
            return View(response.Data);
        }

        public async Task<IActionResult> CreateCertificate()
        {
            var users = await _userService.GetAllAsync();
            var types = await _certificateTypeService.GetAllAsync();

            var vm = new CertificateCreateViewModel
            {
                TumKullanicilar = users.Data ?? new(),
                SertifikaTurleri = types.Data ?? new()
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCertificate(CertificateCreateViewModel vm)
        {
            await _certificateService.CreateAsync(vm.Sertifika);
            return RedirectToAction(nameof(Index), new { basarili = "olusturuldu" });
        }

        public async Task<IActionResult> UpdateCertificate(int id)
        {
            var response = await _certificateService.GetByIdAsync(id);
            return View(response.Data);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCertificate(UpdateCertificateDto updateDto)
        {
            await _certificateService.UpdateAsync(updateDto);
            return RedirectToAction(nameof(Index), new { basarili = "guncellendi" });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCertificate(int id)
        {
            await _certificateService.DeleteAsync(id);
            return RedirectToAction(nameof(Index), new { basarili = "silindi" });
        }


        // Yaklaşan (30 gün içinde) süresi dolacak sertifikalar
        public async Task<IActionResult> ExpiringCertificates(int days = 30)
        {
            var response = await _certificateService.GetUpcomingSoonAsync(days);
            return View(response.Data ?? new());
        }

        // Kullanıcı listesinden gelen id'ye göre sadece o personelin sertifikalarını listeler
        public async Task<IActionResult> UserCertificates(int userId)
        {
            var response = await _certificateService.GetByUserIdAsync(userId);

            // Eğer o personelin hiç sertifikası yoksa boş liste döndür
            return View(response.Data ?? new());
        }

    }
}