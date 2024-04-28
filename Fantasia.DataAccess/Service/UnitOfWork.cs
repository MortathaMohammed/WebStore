using Fantasia.DataAccess.Data;
using Fantasia.DataAccess.Repository.IRepository;
using Fantasia.DataAccess.Service.IService;

namespace Fantasia.DataAccess.Service;
public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _dbContext;
    public IProductService ProductService { get; set; }

    public ICategoryService CategoryService { get; set; }

    public IColorService ColorService { get; set; }

    public ISizeService SizeService { get; set; }

    public UnitOfWork(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        CategoryService = new CategoryService(_dbContext);
        ColorService = new ColorService(_dbContext);
        ProductService = new ProductService(_dbContext);
        SizeService = new SizeService(_dbContext);

    }


    public async void Save()
    {
        await _dbContext.SaveChangesAsync();
    }
}