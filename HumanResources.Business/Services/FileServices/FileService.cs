using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace HumanResources.Business.Services.FileServices
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<string> UploadFileAsync(IFormFile file, string folderName, string customFileName = null)
        {
            if (file == null || file.Length == 0)
                return null;

            var rootPath = _webHostEnvironment.WebRootPath ?? Path.Combine(_webHostEnvironment.ContentRootPath, "wwwroot");
            string uploadsFolder = Path.Combine(rootPath, "uploads", folderName);

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            // 1. Dosyanýn orijinal uzantýsýný alýyoruz (Örn: ".png" veya ".pdf")
            string extension = Path.GetExtension(file.FileName);

            // 2. Eðer customFileName verildiyse onu kullan, verilmediyse dosyanýn orijinal adýný kullan
            string baseName = string.IsNullOrWhiteSpace(customFileName)
                ? Path.GetFileNameWithoutExtension(file.FileName)
                : customFileName;

            // Boþluklarý alt tire ile deðiþtirelim ki URL temiz kalsýn
            baseName = baseName.Replace(" ", "_");

            // 3. Guid + Bizim Ýsim + Uzantý formatýnda birleþtiriyoruz
            string uniqueFileName = $"{Guid.NewGuid()}_{baseName}{extension}";

            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return $"/uploads/{folderName}/{uniqueFileName}";
        }
        public void DeleteFile(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                return;

            // Baþýndaki '/' iþaretini kaldýrýp wwwroot ile birleþtiriyoruz
            var relativePath = filePath.TrimStart('/');
            var fullPath = Path.Combine(_webHostEnvironment.WebRootPath, relativePath);

            // Eðer dosya sunucuda gerçekten varsa, siliyoruz
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }
    }
}