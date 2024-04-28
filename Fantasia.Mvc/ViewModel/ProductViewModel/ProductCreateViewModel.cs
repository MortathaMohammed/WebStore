using System.ComponentModel.DataAnnotations.Schema;
using Fantasia.DataAccess.Entity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Fantasia.Mvc.ViewModel.ProductViewModel;
public class ProductCreateViewModel
{
    public string? Name { get; set; }
    public string? Price { get; set; }

    [NotMapped]
    public IFormFile? Image { get; set; }
    public string? ImageUrl { get; set; }

    public List<SelectListItem>? Colours { get; set; }
    public int[]? SelectedColours { get; set; }

    public List<SelectListItem>? Sizes { get; set; }
    public int[]? SelectedSizes { get; set; }

    public int CategoryId { get; set; }
    public string? Made { get; set; }
}