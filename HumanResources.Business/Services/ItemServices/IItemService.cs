using HumanResources.Business.Base;
using HumanResources.Business.DTOs.ItemDtos;
using HumanResources.Business.Services.GenericServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HumanResources.Business.Services.ItemServices
{
    public interface IItemService : IGenericService<ResultItemDto, CreateItemDto, UpdateItemDto>
    {
        Task<BaseResult<List<ItemDto>>> GetAllItemsWithDetailsAsync();
        Task<BaseResult<List<ItemDto>>> GetItemsByUserIdAsync(int userId);
        Task<BaseResult<ItemDto>> GetItemWithDetailsByIdAsync(int id);
    }
}