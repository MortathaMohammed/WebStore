using Fantasia.DataAccess.Data;
using Fantasia.DataAccess.Entity;
using Fantasia.DataAccess.Repository;
using Fantasia.DataAccess.Repository.IRepository;
using Fantasia.DataAccess.Service.IService;
using Microsoft.EntityFrameworkCore;

namespace Fantasia.DataAccess.Service;
public class ColorService : GenericRepository<Color>, IColorService
{
    private readonly ApplicationDbContext _dbContext;
    public ColorService(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<string> CreateColor(Color color)
    {
        var existingColore = GetTableNoTracking().Any(std => std.Name == color.Name);
        if (existingColore) return "Exists";
        await base.AddAsync(color);
        return "Success";
    }

    public async Task<string> DeleteColor(Color color)
    {
        var trans = BeginTransaction();
        try
        {
            await DeleteAsync(color);

            await trans.CommitAsync();
            return "Success";

        }
        catch
        {
            await trans.RollbackAsync();
            return "Falied";
        }
    }

    public async Task<string> EditColor(Color color)
    {
        await UpdateAsync(color);
        return "Success";
    }

    public async Task<Color> GetColor(int id)
    {
        var product = GetTableNoTracking()
                                        .Where(p => p.Id.Equals(id))
                                        .FirstOrDefault();
        return product!;
    }

    public async Task<List<Color>> GetColours()
    {
        var colores = GetTableNoTracking();
        return await colores.ToListAsync();
    }
}