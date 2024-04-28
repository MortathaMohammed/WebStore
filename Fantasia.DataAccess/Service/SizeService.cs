using Fantasia.DataAccess.Data;
using Fantasia.DataAccess.Entity;
using Fantasia.DataAccess.Repository;
using Fantasia.DataAccess.Service.IService;
using Microsoft.EntityFrameworkCore;

namespace Fantasia.DataAccess.Service;
public class SizeService : GenericRepository<Size>, ISizeService
{
    private readonly ApplicationDbContext _dbContext;
    public SizeService(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<string> CreateSize(Size size)
    {
        var existingSize = GetTableNoTracking().Any(sz => sz.Name == size.Name);
        if (existingSize) return "Exists";
        await base.AddAsync(size);
        return "Success";
    }

    public async Task<string> DeleteSize(Size size)
    {
        var trans = BeginTransaction();
        try
        {
            await DeleteAsync(size);

            await trans.CommitAsync();
            return "Success";

        }
        catch
        {
            await trans.RollbackAsync();
            return "Falied";
        }
    }

    public async Task<string> EditSize(Size size)
    {
        await UpdateAsync(size);
        return "Success";
    }

    public async Task<Size> GetSize(int id)
    {
        var size = GetTableNoTracking()
                                        .Where(p => p.Id.Equals(id))
                                        .FirstOrDefault();
        return size!;
    }

    public async Task<List<Size>> GetSizes()
    {
        var sizes = GetTableNoTracking();
        return await sizes.ToListAsync();
    }
}