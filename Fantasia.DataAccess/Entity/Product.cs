using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace Fantasia.DataAccess.Entity;
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Price { get; set; }

    [NotMapped]
    public IFormFile Image { get; set; }
    public string ImageUrl { get; set; }

    public int ColoreId { get; set; }
    [ForeignKey("ColoreId")]
    public Colore Colore { get; set; }

    public int SizeId { get; set; }
    [ForeignKey("SizeId")]
    public Size Size { get; set; }

    public string Made { get; set; }
}