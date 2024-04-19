namespace Fantasia.DataAccess.Entity;
public class Size
{

    public int Id { get; set; }
    public string Name { get; set; }
    public List<Product> Products { get; set; }
}