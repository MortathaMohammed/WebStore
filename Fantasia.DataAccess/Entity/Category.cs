using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace Fantasia.DataAccess.Entity;
public class Category
{
    public int Id { get; set; }
    public string? Name { get; set; }
    [NotMapped]
    public IFormFile? Image { get; set; }
    public string? ImageUrl { get; set; }
    public List<Product>? Products { get; set; }
}