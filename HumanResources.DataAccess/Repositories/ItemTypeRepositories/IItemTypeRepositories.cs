using HumanResources.DataAccess.Repositories;
using HumanResources.Entity.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HumanResources.DataAccess.Repositories.ItemTypeRepositories
{
    public interface IItemTypeRepository : IGenericRepository<ZimmetTuru>
    {
        Task<List<ZimmetTuru>> GetAllItemTypesWithItemsAsync();

        Task<ZimmetTuru> GetItemTypeWithItemsByIdAsync(int id);
    }
}