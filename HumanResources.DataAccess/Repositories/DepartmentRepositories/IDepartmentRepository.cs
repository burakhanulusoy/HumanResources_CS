using HumanResources.Entity.Entities;

namespace HumanResources.DataAccess.Repositories.DepartmentRepositories
{
    public interface IDepartmentRepository:IGenericRepository<Departman>
    {
        // Task<List<Departman>> GetAllWithDetailsAsync();
        // Task<Departman?> GetDetailsByIdAsync(int id);

        Task<List<Departman>> GetDepartmentsWithUserAsync();
        Task<Departman> GetDepartmentWithUserAsync(int id);
        Task<Departman> GetDepartmentWithUnitsAsync(int id);


    }
}
