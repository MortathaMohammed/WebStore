using Fantasia.DataAccess.Data;
using Fantasia.DataAccess.Entity;
using Fantasia.DataAccess.Repository;
using Fantasia.DataAccess.Service.IService;

namespace Fantasia.DataAccess.Service;
public class SizeService : GenericRepository<Size>, ISizeService
{
    public SizeService(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}