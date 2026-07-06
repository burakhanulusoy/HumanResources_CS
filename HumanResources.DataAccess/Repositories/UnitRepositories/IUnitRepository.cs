using HumanResources.Entity.Entities;

namespace HumanResources.DataAccess.Repositories.UnitRepositories
{
    public interface IUnitRepository :IGenericRepository<Birim>
    {
        Task<Birim> GetUnitWithUsersAsync(int unitId);

    }
}
