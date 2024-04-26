using Fantasia.DataAccess.Data;
using Fantasia.DataAccess.Entity;
using Fantasia.DataAccess.Repository;
using Fantasia.DataAccess.Service.IService;
using Microsoft.EntityFrameworkCore;

namespace Fantasia.DataAccess.Service;
public class CategoryService : GenericRepository<Category>, ICategoryService
{
    public CategoryService(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<string> CreateCategory(Category category)
    {
        var existingCategory = GetTableNoTracking().Any(c => c.Name == category.Name);
        if (existingCategory) return "Exists";
        await base.AddAsync(category);
        return "Success";
    }

    public async Task<string> DeleteCategory(Category category)
    {
        var trans = BeginTransaction();
        try
        {
            await DeleteAsync(category);

            await trans.CommitAsync();
            return "Success";

        }
        catch
        {
            await trans.RollbackAsync();
            return "Falied";
        }

    }

    public async Task<string> EditCategory(Category category)
    {
        await UpdateAsync(category);
        return "Success";
    }

    public async Task<List<Category>> GetCategories()
    {
        var categories = GetTableNoTracking();
        return await categories.ToListAsync();
    }

    public async Task<Category> GetCategory(int id)
    {
        var category = GetTableNoTracking().Where(c => c.Id == id).FirstOrDefault();
        return category!;
    }
}