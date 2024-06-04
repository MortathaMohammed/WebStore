using System.ComponentModel.DataAnnotations.Schema;
using Fantasia.DataAccess.Entity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Fantasia.Mvc.Models.ViewModel.ProductViewModel;
public class ProductViewModel
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Price { get; set; }

    [NotMapped]
    public IFormFile? Image { get; set; }
    public string? ImageUrl { get; set; }

    public List<SelectListItem>? Colours { get; set; }
    public string[]? SelectedColours { get; set; }

    public List<SelectListItem>? Sizes { get; set; }
    public string[]? SelectedSizes { get; set; }

    public List<ProductSize> ProductSizes { get; set; } = new List<ProductSize>();

    public List<ProductColor> ProductColours { get; set; } = new List<ProductColor>();

    public int CategoryId { get; set; }
    [ForeignKey("CategoryId")]
    public Category? Category { get; set; }

    public string? Made { get; set; }
}