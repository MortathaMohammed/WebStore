using Fantasia.DataAccess.Data;
using Fantasia.DataAccess.Service.IService;

namespace Fantasia.DataAccess.Service;
public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _dbContext;

    public UnitOfWork(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;

    }

    public void Save()
    {
        throw new NotImplementedException();
    }
}