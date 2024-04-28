using AutoMapper;
using Fantasia.DataAccess.Entity;
using Fantasia.Mvc.ViewModel.ProductViewModel;

namespace Fantasia.Mvc.Helpers;
public class Helper : Profile
{
    public Helper()
    {
        CreateMap<Product, ProductViewModel>();
        CreateMap<ProductViewModel, Product>();
    }

}