using Fantasia.DataAccess.Entity;

namespace Fantasia.DataAccess.Service.IService;
public interface ISizeService
{
    public Task<List<Size>> GetSizes();
    public Task<Size> GetSize(int id);
    public Task<string> CreateSize(Size size);
    public Task<string> EditSize(Size size);
    public Task<string> DeleteSize(Size size);
}