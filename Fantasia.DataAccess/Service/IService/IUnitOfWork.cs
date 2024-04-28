namespace Fantasia.DataAccess.Service.IService;
public interface IUnitOfWork
{
    IProductService ProductService { get; }
    ICategoryService CategoryService { get; }
    IColorService ColorService { get; }
    ISizeService SizeService { get; }
    void Save();
}