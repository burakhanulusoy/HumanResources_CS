using HumanResources.Entity.Entities;

namespace HumanResources.DataAccess.Repositories.DemirbasRepositories
{
    public interface IDemirbasRepository : IGenericRepository<Demirbas>
    {
        Task<List<Demirbas>> GetAllWithTypeAsync();
        Task<List<Demirbas>> GetAvailableAsync();          // Zimmet formu dropdown'u için (Müsait olanlar)
        Task<Demirbas> GetByIdWithTypeAsync(int id);
        Task<bool> HasAnyZimmetAsync(int demirbasId);
    }
}