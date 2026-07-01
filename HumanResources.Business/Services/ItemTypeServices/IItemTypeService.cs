using HumanResources.Business.Base;
using HumanResources.Business.DTOs.ItemTypeDtos;
using HumanResources.Business.Services.GenericServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HumanResources.Business.Services.ItemTypeServices
{
    public interface IItemTypeService : IGenericService<ResultItemTypeDto, CreateItemTypeDto, UpdateItemTypeDto>
    {
        Task<BaseResult<List<ItemTypeDto>>> GetAllItemTypesWithItemsAsync();
        Task<BaseResult<ItemTypeDto>> GetItemTypeWithItemsByIdAsync(int id);
    }
}