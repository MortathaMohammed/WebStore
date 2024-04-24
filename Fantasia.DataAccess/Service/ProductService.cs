using Fantasia.DataAccess.Data;
using Fantasia.DataAccess.Entity;
using Fantasia.DataAccess.Repository;
using Fantasia.DataAccess.Service.IService;

namespace Fantasia.DataAccess.Service;
public class ProductService : GenericRepository<Product>, IProductService
{
    public ProductService(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}