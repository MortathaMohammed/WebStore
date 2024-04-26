using Fantasia.DataAccess.Entity;

namespace Fantasia.DataAccess.Service.IService;
public interface IProductService
{
    public Task<List<Product>> GetProducts();
    public Task<Product> GetProduct(int id);
    public Task<string> CreateProduct(Product product);
    public Task<string> EditProduct(Product product);
    public Task<string> DeleteProduct(Product product);
}
