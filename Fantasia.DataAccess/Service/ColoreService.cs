using Fantasia.DataAccess.Data;
using Fantasia.DataAccess.Entity;
using Fantasia.DataAccess.Repository;
using Fantasia.DataAccess.Repository.IRepository;
using Fantasia.DataAccess.Service.IService;
using Microsoft.EntityFrameworkCore;

namespace Fantasia.DataAccess.Service;
public class ColoreService : GenericRepository<Colore>, IColoreService
{
    private readonly ApplicationDbContext _dbContext;
    public ColoreService(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
    public Task<Colore> GetColore(int id)
    {
        throw new NotImplementedException();
    }
}