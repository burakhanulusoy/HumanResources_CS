using HumanResouerces.WebUI.Base;
using HumanResouerces.WebUI.Exceptions;
using HumanResources.Business.DTOs.UserDtos;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace HumanResources.WebUI.Services.UserServices
{
    public class UserService(HttpClient _client) : IUserService
    {
        public async Task<BaseResult<object>> CreateAsync(CreateUserDto createDto)
        {
            using var content = new MultipartFormDataContent();

            // Fotoðraf ekleme iþlemi
            if (createDto.Fotograf is not null)
            {
                var streamContent = new StreamContent(createDto.Fotograf.OpenReadStream());
                streamContent.Headers.ContentType = new MediaTypeHeaderValue(createDto.Fotograf.ContentType);
                content.Add(streamContent, "Fotograf", createDto.Fotograf.FileName);
            }

            // Metin bazlý deðerler
            content.Add(new StringContent(createDto.UserName ?? ""), "UserName");
            content.Add(new StringContent(createDto.Email ?? ""), "Email");
            content.Add(new StringContent(createDto.Password ?? ""), "Password");

            if (createDto.DogumTarihi.HasValue)
            {
                content.Add(new StringContent(createDto.DogumTarihi.Value.ToString("yyyy-MM-dd")), "DogumTarihi");
            }

            // NOT: DTO'ndaki diðer alanlarý (Ad, Soyad, TcKimlikNo vb.) buraya benzer þekilde StringContent olarak eklemelisin.
            // Örn: content.Add(new StringContent(createDto.Ad ?? ""), "Ad");

            var response = await _client.PostAsync("users", content);
            var result = await response.Content.ReadFromJsonAsync<BaseResult<object>>();

            return result.IsFailure ? throw new ApiValidationException(result.Errors ?? new List<Error>()) : result;
        }

        public async Task<BaseResult<object>> DeleteAsync(int id)
        {
            var response = await _client.DeleteAsync($"users/{id}");

            return await response.Content.ReadFromJsonAsync<BaseResult<object>>();
        }

        public async Task<BaseResult<List<UserDto>>> GetAllAsync()
        {
            return await _client.GetFromJsonAsync<BaseResult<List<UserDto>>>("users");
        }

        // IGenericService arayüz kuralýna göre UpdateUserDto dönülüyor
        public async Task<BaseResult<UpdateUserDto>> GetByIdAsync(int id)
        {
            return await _client.GetFromJsonAsync<BaseResult<UpdateUserDto>>($"users/{id}");
        }

        public async Task<BaseResult<object>> UpdateAsync(UpdateUserDto updateDto)
        {
            using var content = new MultipartFormDataContent();

            if (updateDto.Fotograf is not null)
            {
                var streamContent = new StreamContent(updateDto.Fotograf.OpenReadStream());
                streamContent.Headers.ContentType = new MediaTypeHeaderValue(updateDto.Fotograf.ContentType);
                content.Add(streamContent, "Fotograf", updateDto.Fotograf.FileName);
            }

            content.Add(new StringContent(updateDto.Id.ToString()), "Id");
            content.Add(new StringContent(updateDto.UserName ?? ""), "UserName");
            content.Add(new StringContent(updateDto.Email ?? ""), "Email");

            if (updateDto.DogumTarihi.HasValue)
            {
                content.Add(new StringContent(updateDto.DogumTarihi.Value.ToString("yyyy-MM-dd")), "DogumTarihi");
            }

            // NOT: Güncelleme için diðer DTO alanlarýný da buraya eklemelisin.

            var response = await _client.PutAsync("users", content);
            var result = await response.Content.ReadFromJsonAsync<BaseResult<object>>();

            return result.IsFailure ? throw new ApiValidationException(result.Errors ?? new List<Error>()) : result;
        }

        // --- Özel Metotlar ---

        public async Task<BaseResult<List<ResultUserDto>>> GetAllWithDepartmentAndUnitAsync()
        {
            return await _client.GetFromJsonAsync<BaseResult<List<ResultUserDto>>>("users/WithDepartmentAndUnit");
        }

        public async Task<BaseResult<ResultUserDto>> GetByIdWithDepartmentAndUnitAsync(int id)
        {
            // API tarafýnda route içinde {id} belirtilmediði için (sadece "ByIdWithDepartmentAndUnit") querystring olarak gönderiyoruz
            return await _client.GetFromJsonAsync<BaseResult<ResultUserDto>>($"users/ByIdWithDepartmentAndUnit?id={id}");
        }

        public async Task<BaseResult<List<ResultUserDto>>> GetSubordinatesAsync(int amirId)
        {
            return await _client.GetFromJsonAsync<BaseResult<List<ResultUserDto>>>($"users/GetSubordinates/{amirId}");
        }

        public async Task<BaseResult<List<UserDto>>> GetUsersByRoleAsync(string roleName)
        {
            return await _client.GetFromJsonAsync<BaseResult<List<UserDto>>>($"users/GetUsersByRole/{roleName}");
        }

        // HumanResources.WebUI.Services.UserServices.UserService içerisine eklenecek:
        public async Task<BaseResult<List<ResultUserDto>>> GetUsersByUnitIdAsync(int unitId)
        {
            return await _client.GetFromJsonAsync<BaseResult<List<ResultUserDto>>>($"users/GetUsersByUnit/{unitId}");
        }

        public async Task<BaseResult<List<ResultUserDto>>> GetAllUsersWithRolesAsync()
        {
            return await _client.GetFromJsonAsync<BaseResult<List<ResultUserDto>>>("users/WithRoles");
        }
        public async Task<BaseResult<ResultUserDto>> LoginUserAsync(LoginUserDto loginUserDto)
        {
            var response = await _client.PostAsJsonAsync("users/login", loginUserDto);

            // API'den gelen sonucu direkt okuyoruz
            var result = await response.Content.ReadFromJsonAsync<BaseResult<ResultUserDto>>();

            // UnitService'te olduðu gibi direkt tek satýrda kontrol edip fýrlatýyoruz
            return result.IsFailure ? throw new ApiValidationException(result.Errors) : result;
        }

    }
}