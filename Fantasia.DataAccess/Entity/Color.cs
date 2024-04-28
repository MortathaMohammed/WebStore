namespace Fantasia.DataAccess.Entity;
public class Color
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public ICollection<ProductColor> ProductColours { get; set; } = new List<ProductColor>();
}