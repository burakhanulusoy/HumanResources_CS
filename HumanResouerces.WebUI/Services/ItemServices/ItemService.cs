using HumanResouerces.WebUI.Base;
using HumanResouerces.WebUI.Exceptions;
using HumanResources.Business.DTOs.ItemDtos;

namespace HumanResources.WebUI.Services.ItemServices
{
    public class ItemService(HttpClient _client) : IItemService
    {
        public async Task<BaseResult<object>> CreateAsync(CreateItemDto createDto)
        {
            var response = await _client.PostAsJsonAsync("items", createDto);

            var result = await response.Content.ReadFromJsonAsync<BaseResult<object>>();

            return result.IsFailure ? throw new ApiValidationException(result.Errors) : result;
        }

        public async Task<BaseResult<object>> DeleteAsync(int id)
        {
            var response = await _client.DeleteAsync($"items/{id}");

            return await response.Content.ReadFromJsonAsync<BaseResult<object>>();
        }

        public async Task<BaseResult<List<ResultItemDto>>> GetAllAsync()
        {
            return await _client.GetFromJsonAsync<BaseResult<List<ResultItemDto>>>("items");
        }

        public async Task<BaseResult<UpdateItemDto>> GetByIdAsync(int id)
        {
            return await _client.GetFromJsonAsync<BaseResult<UpdateItemDto>>($"items/{id}");
        }

        public async Task<BaseResult<object>> UpdateAsync(UpdateItemDto updateDto)
        {
            var response = await _client.PutAsJsonAsync("items", updateDto);

            var result = await response.Content.ReadFromJsonAsync<BaseResult<object>>();

            return result.IsFailure ? throw new ApiValidationException(result.Errors) : result;
        }

        // --- Özel Metotlar ---

        public async Task<BaseResult<List<ItemDto>>> GetAllItemsWithDetailsAsync()
        {
            return await _client.GetFromJsonAsync<BaseResult<List<ItemDto>>>("items/GetAllWithDetails");
        }

        public async Task<BaseResult<List<ItemDto>>> GetItemsByUserIdAsync(int userId)
        {
            return await _client.GetFromJsonAsync<BaseResult<List<ItemDto>>>($"items/GetByUserId/{userId}");
        }

        public async Task<BaseResult<ItemDto>> GetItemWithDetailsByIdAsync(int id)
        {
            return await _client.GetFromJsonAsync<BaseResult<ItemDto>>($"items/GetWithDetails/{id}");
        }
    }
}