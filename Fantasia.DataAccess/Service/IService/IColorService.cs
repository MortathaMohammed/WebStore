using Fantasia.DataAccess.Entity;
using Fantasia.DataAccess.Repository.IRepository;

namespace Fantasia.DataAccess.Service.IService;
public interface IColorService : IGenericRepository<Color>
{
    public Task<List<Color>> GetColours();
    public Task<Color> GetColor(int id);
    public Task<string> CreateColor(Color color);
    public Task<string> EditColor(Color color);
    public Task<string> DeleteColor(Color color);
}