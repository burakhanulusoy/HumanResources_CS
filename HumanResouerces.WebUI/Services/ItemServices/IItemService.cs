using HumanResouerces.WebUI.Base;
using HumanResources.WebUI.DTOs.ItemDtos;
using HumanResources.WebUI.Services.GenericServices;

namespace HumanResources.WebUI.Services.ItemServices
{
    public interface IItemService : IGenericService<ResultItemDto, CreateItemDto, UpdateItemDto>
    {
        Task<BaseResult<List<ItemDto>>> GetAllItemsWithDetailsAsync();
        Task<BaseResult<List<ItemDto>>> GetItemsByUserIdAsync(int userId);
        Task<BaseResult<ItemDto>> GetItemWithDetailsByIdAsync(int id);
    }
}