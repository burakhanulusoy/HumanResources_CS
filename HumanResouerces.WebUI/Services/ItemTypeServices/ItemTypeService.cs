using HumanResouerces.WebUI.Base;
using HumanResouerces.WebUI.Exceptions;
using HumanResources.Business.DTOs.ItemTypeDtos;

namespace HumanResources.WebUI.Services.ItemTypeServices
{
    public class ItemTypeService(HttpClient _client) : IItemTypeService
    {
        public async Task<BaseResult<object>> CreateAsync(CreateItemTypeDto createDto)
        {
            var response = await _client.PostAsJsonAsync("itemtypes", createDto);

            var result = await response.Content.ReadFromJsonAsync<BaseResult<object>>();

            return result.IsFailure ? throw new ApiValidationException(result.Errors) : result;
        }

        public async Task<BaseResult<object>> DeleteAsync(int id)
        {
            var response = await _client.DeleteAsync($"itemtypes/{id}");

            return await response.Content.ReadFromJsonAsync<BaseResult<object>>();
        }

        public async Task<BaseResult<List<ResultItemTypeDto>>> GetAllAsync()
        {
            return await _client.GetFromJsonAsync<BaseResult<List<ResultItemTypeDto>>>("itemtypes");
        }

        public async Task<BaseResult<UpdateItemTypeDto>> GetByIdAsync(int id)
        {
            return await _client.GetFromJsonAsync<BaseResult<UpdateItemTypeDto>>($"itemtypes/{id}");
        }

        public async Task<BaseResult<object>> UpdateAsync(UpdateItemTypeDto updateDto)
        {
            var response = await _client.PutAsJsonAsync("itemtypes", updateDto);

            var result = await response.Content.ReadFromJsonAsync<BaseResult<object>>();

            return result.IsFailure ? throw new ApiValidationException(result.Errors) : result;
        }

        // --- Özel Metotlar ---

        public async Task<BaseResult<List<ItemTypeDto>>> GetAllWithItemsAsync()
        {
            return await _client.GetFromJsonAsync<BaseResult<List<ItemTypeDto>>>("itemtypes/GetAllWithItems");
        }

        public async Task<BaseResult<ItemTypeDto>> GetWithItemsAsync(int id)
        {
            return await _client.GetFromJsonAsync<BaseResult<ItemTypeDto>>($"itemtypes/GetWithItems/{id}");
        }
    }
}