using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace HumanResources.Business.Services.FileServices
{
    public interface IFileService
    {
      
        Task<string> UploadFileAsync(IFormFile file, string folderName, string customFileName = null);
        void DeleteFile(string filePath);
    }
}