using HumanResouerces.WebUI.Base;
using HumanResources.WebUI.DTOs.ItemTypeDtos;
using HumanResources.WebUI.Services.GenericServices;

namespace HumanResources.WebUI.Services.ItemTypeServices
{
    public interface IItemTypeService : IGenericService<ResultItemTypeDto, CreateItemTypeDto, UpdateItemTypeDto>
    {
        Task<BaseResult<List<ItemTypeDto>>> GetAllWithItemsAsync();
        Task<BaseResult<ItemTypeDto>> GetWithItemsAsync(int id);
    }
}