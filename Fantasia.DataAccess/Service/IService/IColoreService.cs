using Fantasia.DataAccess.Entity;
using Fantasia.DataAccess.Repository.IRepository;

namespace Fantasia.DataAccess.Service.IService;
public interface IColoreService : IGenericRepository<Colore>
{
    public Task<Colore> GetColore(int id);
}