using Fantasia.DataAccess.Entity;

namespace Fantasia.Mvc.Models;
public class SizeListSize
{
    public Size Size { get; set; }
    public List<Size> Sizes { get; set; }
}