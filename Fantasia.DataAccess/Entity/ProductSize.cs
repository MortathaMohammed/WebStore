using System.ComponentModel.DataAnnotations.Schema;

namespace Fantasia.DataAccess.Entity;
public class ProductSize
{

    public int Id { get; set; }
    public int ProductId { get; set; }
    [ForeignKey("ProductId")]
    public Product? Product { get; set; }

    public int SizeId { get; set; }
    [ForeignKey("SizeId")]
    public Size? Size { get; set; }
}