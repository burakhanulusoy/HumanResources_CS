using HumanResouerces.WebUI.Base;
using HumanResouerces.WebUI.Exceptions;
using HumanResources.WebUI.DTOs.UserDtos;
using System.Net.Http.Headers;

namespace HumanResources.WebUI.Services.UserServices
{
    public class UserService(HttpClient _client) : IUserService
    {
        public async Task<BaseResult<object>> CreateAsync(CreateUserDto createDto)
        {
            using var content = BuildFormContent(createDto.Fotograf,
                ("UserName", createDto.UserName),
                ("Email", createDto.Email),
                ("Password", createDto.Password),
                ("PhoneNumber", createDto.PhoneNumber),
                ("SicilNo", createDto.SicilNo),
                ("Ad", createDto.Ad),
                ("Soyad", createDto.Soyad),
                ("TcKimlikNo", createDto.TcKimlikNo),
                ("DogumTarihi", createDto.DogumTarihi?.ToString("yyyy-MM-dd")),
                ("Cinsiyet", createDto.Cinsiyet),
                ("MedeniDurum", createDto.MedeniDurum),
                ("KanGrubu", createDto.KanGrubu),
                ("Adres", createDto.Adres),
                ("AcilDurumKisiAdSoyad", createDto.AcilDurumKisiAdSoyad),
                ("AcilDurumTelefonu", createDto.AcilDurumTelefonu),
                ("DepartmanId", createDto.DepartmanId?.ToString()),
                ("BirimId", createDto.BirimId?.ToString()),
                ("AmirId", createDto.AmirId?.ToString()),
                ("VardiyaId", createDto.VardiyaId?.ToString()),
                ("CalismaDurumu", createDto.CalismaDurumu),
                ("PersonelTipi", createDto.PersonelTipi),
                ("SgkSicilNo", createDto.SgkSicilNo)
            );

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

        public async Task<BaseResult<UpdateUserDto>> GetByIdAsync(int id)
        {
            return await _client.GetFromJsonAsync<BaseResult<UpdateUserDto>>($"users/{id}");
        }

        public async Task<BaseResult<object>> UpdateAsync(UpdateUserDto updateDto)
        {
            using var content = BuildFormContent(updateDto.Fotograf,
                ("Id", updateDto.Id?.ToString()),
                ("UserName", updateDto.UserName),
                ("Email", updateDto.Email),
                ("Password", updateDto.Password),
                ("PhoneNumber", updateDto.PhoneNumber),
                ("SicilNo", updateDto.SicilNo),
                ("Ad", updateDto.Ad),
                ("Soyad", updateDto.Soyad),
                ("TcKimlikNo", updateDto.TcKimlikNo),
                ("DogumTarihi", updateDto.DogumTarihi?.ToString("yyyy-MM-dd")),
                ("Cinsiyet", updateDto.Cinsiyet),
                ("MedeniDurum", updateDto.MedeniDurum),
                ("KanGrubu", updateDto.KanGrubu),
                ("Adres", updateDto.Adres),
                ("AcilDurumKisiAdSoyad", updateDto.AcilDurumKisiAdSoyad),
                ("AcilDurumTelefonu", updateDto.AcilDurumTelefonu),
                ("DepartmanId", updateDto.DepartmanId?.ToString()),
                ("BirimId", updateDto.BirimId?.ToString()),
                ("AmirId", updateDto.AmirId?.ToString()),
                ("VardiyaId", updateDto.VardiyaId?.ToString()),
                ("CalismaDurumu", updateDto.CalismaDurumu),
                ("PersonelTipi", updateDto.PersonelTipi),
                ("SgkSicilNo", updateDto.SgkSicilNo)
            );

            var response = await _client.PutAsync("users", content);
            var result = await response.Content.ReadFromJsonAsync<BaseResult<object>>();

            return result.IsFailure ? throw new ApiValidationException(result.Errors ?? new List<Error>()) : result;
        }

        // --- Özel Metotlar (deðiþmedi) ---

        public async Task<BaseResult<List<ResultUserDto>>> GetAllWithDepartmentAndUnitAsync()
        {
            return await _client.GetFromJsonAsync<BaseResult<List<ResultUserDto>>>("users/WithDepartmentAndUnit");
        }

        public async Task<BaseResult<ResultUserDto>> GetByIdWithDepartmentAndUnitAsync(int id)
        {
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
            var result = await response.Content.ReadFromJsonAsync<BaseResult<ResultUserDto>>();
            return result.IsFailure ? throw new ApiValidationException(result.Errors) : result;
        }

        // --- Ortak multipart builder ---
        private static MultipartFormDataContent BuildFormContent(IFormFile? dosya, params (string Key, string? Value)[] fields)
        {
            var content = new MultipartFormDataContent();

            foreach (var (key, value) in fields)
                content.Add(new StringContent(value ?? string.Empty), key);

            if (dosya is not null && dosya.Length > 0)
            {
                var streamContent = new StreamContent(dosya.OpenReadStream());
                streamContent.Headers.ContentType = new MediaTypeHeaderValue(dosya.ContentType);
                content.Add(streamContent, "Fotograf", dosya.FileName);
            }

            return content;
        }
    }
}