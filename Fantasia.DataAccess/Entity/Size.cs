namespace Fantasia.DataAccess.Entity;
public class Size
{

    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<ProductSize> ProductSizes { get; set; } = new List<ProductSize>();
}