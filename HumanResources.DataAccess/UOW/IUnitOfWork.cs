namespace HumanResources.DataAccess.UOW
{
    public interface IUnitOfWork
    {
        Task<bool> SaveChangesAsync();





    }
}
