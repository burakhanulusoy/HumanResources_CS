using HumanResources.Entity.Entities;

namespace HumanResources.DataAccess.Repositories.DiciplineRepositories
{
    public interface IDiciplineRepository:IGenericRepository<DisiplinKaydi>
    {
        Task<List<DisiplinKaydi>> GetByUserIdAsync(int  userId);
        // IDiciplineRepository.cs — ekle
        Task<List<DisiplinKaydi>> GetAllWithUserAsync();
    }

}
