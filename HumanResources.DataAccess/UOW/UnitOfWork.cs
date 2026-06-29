using HumanResources.DataAccess.Context;

namespace HumanResources.DataAccess.UOW
{
    public class UnitOfWork(AppDbContext _context):IUnitOfWork
    {
        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;//0 dan buyukse true doner kayýt etkýlenem durumu 
        }
    }
}
