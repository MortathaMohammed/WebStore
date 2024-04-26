namespace Fantasia.DataAccess.Service.IService;
public interface IUnitOfWork
{
    IProductService ProductService { get; }
    ICategoryService CategoryService { get; }
    IColoreService ColoreService { get; }
    ISizeService SizeService { get; }
    void Save();
}