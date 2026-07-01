using HumanResources.DataAccess.Repositories;
using HumanResources.Entity.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HumanResources.DataAccess.Repositories.ItemRepositories
{
    public interface IItemRepository : IGenericRepository<Zimmet>
    {
        // Admin paneli için tüm eşyaları, kimde oldukları ve türleriyle listeleme
        Task<List<Zimmet>> GetAllItemsWithDetailsAsync();

        // Personel kendi profiline girdiğinde sadece ondaki eşyaları listeleme
        Task<List<Zimmet>> GetItemsByUserIdAsync(int userId);

        // İSTEDİĞİN ÖZEL METOT: Tek bir spesifik eşyaya (Örn Id: 5) tıklanınca detayını ve kimin aldığını görme
        Task<Zimmet> GetItemWithDetailsByIdAsync(int id);
    }
}