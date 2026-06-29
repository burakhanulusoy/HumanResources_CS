using HumanResources.Entity.Entities.Common;

namespace HumanResources.DataAccess.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        Task<List<TEntity>> GetAllAsync();
        IQueryable<TEntity> GetQueryable();
        Task<TEntity> GetByIdAsync(int id);
        void Delete(TEntity entity);
        void Update(TEntity entity);
        Task CreateAsync(TEntity entity);



    }
}
