namespace Fantasia.DataAccess.Entity;
public class Colore
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public ICollection<ProductColor> ProductColours { get; set; } = new List<ProductColor>();
}