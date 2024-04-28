using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Fantasia.DataAccess.Entity;
public class Product
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Price { get; set; }

    public string? ImageUrl { get; set; }

    public ICollection<ProductSize> ProductSizes { get; set; } = new List<ProductSize>();

    public ICollection<ProductColor> ProductColours { get; set; } = new List<ProductColor>();

    public int CategoryId { get; set; }
    [ForeignKey("CategoryId")]
    public Category? Category { get; set; }

    public string? Made { get; set; }
}