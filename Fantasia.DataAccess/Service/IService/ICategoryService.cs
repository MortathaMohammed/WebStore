using Fantasia.DataAccess.Entity;

namespace Fantasia.DataAccess.Service.IService;
public interface ICategoryService
{
    Task<List<Category>> GetCategories();
    Task<Category> GetCategory(int id);
    Task<string> CreateCategory(Category category);
    Task<string> EditCategory(Category category);
    Task<string> DeleteCategory(Category category);
}