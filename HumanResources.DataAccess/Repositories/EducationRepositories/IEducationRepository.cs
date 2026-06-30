using HumanResources.Entity.Entities;

namespace HumanResources.DataAccess.Repositories.EducationRepositories
{
    public interface IEducationRepository :IGenericRepository<Egitim>
    {
        Task<List<Egitim>> GetAllEducationWithUserAsync();
        Task<Egitim> GetEducationWithUserAsync(int id);



    }
}
