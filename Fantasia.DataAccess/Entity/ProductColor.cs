using System.ComponentModel.DataAnnotations.Schema;

namespace Fantasia.DataAccess.Entity;
public class ProductColor
{
    public int Id { get; set; }

    public int ProductId { get; set; }
    [ForeignKey("ProductId")]
    public Product? Product { get; set; }

    public int ColorId { get; set; }
    [ForeignKey("ColorId")]
    public Color? Color { get; set; }
}