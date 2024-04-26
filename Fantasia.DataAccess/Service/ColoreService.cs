using Fantasia.DataAccess.Data;
using Fantasia.DataAccess.Entity;
using Fantasia.DataAccess.Repository;
using Fantasia.DataAccess.Repository.IRepository;
using Fantasia.DataAccess.Service.IService;
using Microsoft.EntityFrameworkCore;

namespace Fantasia.DataAccess.Service;
public class ColoreService : GenericRepository<Colore>, IColoreService
{
    private readonly ApplicationDbContext _dbContext;
    public ColoreService(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<string> CreateColore(Colore colore)
    {
        var existingColore = GetTableNoTracking().Any(std => std.Name == std.Name);
        if (existingColore) return "Exists";
        await base.AddAsync(colore);
        return "Success";
    }

    public async Task<string> DeleteColore(Colore colore)
    {
        var trans = BeginTransaction();
        try
        {
            await DeleteAsync(colore);

            await trans.CommitAsync();
            return "Success";

        }
        catch
        {
            await trans.RollbackAsync();
            return "Falied";
        }
    }

    public async Task<string> EditColore(Colore colore)
    {
        await UpdateAsync(colore);
        return "Success";
    }

    public async Task<Colore> GetColore(int id)
    {
        var product = GetTableNoTracking()
                                        .Where(p => p.Id.Equals(id))
                                        .FirstOrDefault();
        return product!;
    }

    public async Task<List<Colore>> GetColores()
    {
        var colores = GetTableNoTracking();
        return await colores.ToListAsync();
    }
}