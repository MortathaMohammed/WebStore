using Fantasia.DataAccess.Entity;
using Fantasia.DataAccess.Repository.IRepository;

namespace Fantasia.DataAccess.Service.IService;
public interface IColoreService : IGenericRepository<Colore>
{
    public Task<List<Colore>> GetColores();
    public Task<Colore> GetColore(int id);
    public Task<string> CreateColore(Colore colore);
    public Task<string> EditColore(Colore colore);
    public Task<string> DeleteColore(Colore colore);
}