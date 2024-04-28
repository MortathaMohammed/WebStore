using Fantasia.DataAccess.Data;
using Fantasia.DataAccess.Entity;
using Fantasia.DataAccess.Repository;
using Fantasia.DataAccess.Service.IService;
using Microsoft.EntityFrameworkCore;

namespace Fantasia.DataAccess.Service;
public class ProductService : GenericRepository<Product>, IProductService
{
    private readonly ApplicationDbContext _dbContext;
    public ProductService(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<string> CreateProduct(Product product)
    {
        var existingProduct = GetTableNoTracking().Any(std => std.Name == std.Name);
        if (existingProduct) return "Exists";
        await base.AddAsync(product);
        return "Success";
    }

    public async Task<string> DeleteProduct(Product product)
    {
        var trans = BeginTransaction();
        try
        {
            await DeleteAsync(product);

            await trans.CommitAsync();
            return "Success";

        }
        catch
        {
            await trans.RollbackAsync();
            return "Falied";
        }
    }

    public async Task<string> EditProduct(Product product)
    {
        await UpdateAsync(product);
        return "Success";
    }

    public async Task<Product> GetProduct(int id)
    {
        var product = GetTableNoTracking()
                                        .Include("ProductColor.Colore")
                                        .Include("ProductSize.Size")
                                        .FirstOrDefault(p => p.Id.Equals(id));
        return product!;
    }

    public async Task<List<Product>> GetProducts()
    {
        var products = GetTableNoTracking();
        return await products.ToListAsync();
    }
}