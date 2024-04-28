using System.Linq;
using Fantasia.DataAccess.Data;
using Fantasia.DataAccess.Entity;
using Fantasia.DataAccess.Service.IService;
using Fantasia.Mvc.ViewModel.ProductViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
namespace Fantasia.Mvc.Controllers;
public class ProductController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHostingEnvironment _hostingEnvironment;
    private readonly ApplicationDbContext _dbContext;

    public ProductController(IUnitOfWork unitOfWork,
                             IHostingEnvironment hostingEnvironment,
                             ApplicationDbContext dbContext)
    {
        _unitOfWork = unitOfWork;
        _dbContext = dbContext;
        _hostingEnvironment = hostingEnvironment;
    }
    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        var products = await _unitOfWork.ProductService.GetProducts();
        return View(products);
    }

    [HttpGet]
    public async Task<IActionResult> GetProductById(int id)
    {
        var product = await _unitOfWork.ProductService.GetProduct(id);
        return View(product);
    }

    [HttpGet]
    public async Task<IActionResult> CreateProduct()
    {

        ViewData["ColoreId"] = new SelectList(_dbContext.Colors, "Id", "Name");
        ViewData["SizeId"] = new SelectList(_dbContext.Sizes, "Id", "Name");
        ViewData["CategoryId"] = new SelectList(_dbContext.Categories, "Id", "Name");
        var colours = await _unitOfWork.ColoreService.GetColores();
        var selectColoursList = new List<SelectListItem>();
        foreach (var item in colours)
        {
            selectColoursList.Add(new SelectListItem(item.Id.ToString(), item.Code));
        }
        var sizes = await _unitOfWork.ColoreService.GetColores();
        var selectSizesList = new List<SelectListItem>();
        foreach (var item in sizes)
        {
            selectSizesList.Add(new SelectListItem(item.Id.ToString(), item.Name));
        }

        var product = new ProductCreateViewModel()
        {
            Colours = selectColoursList,
            Sizes = selectSizesList
        };

        return View(product);
    }

    public async Task<IActionResult> CreateProduct(ProductCreateViewModel product)
    {

        string ImageFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
        string imagepath = Path.Combine(ImageFolder, product.Image.FileName);
        product.Image.CopyTo(new FileStream(imagepath, FileMode.Create));
        product.ImageUrl = product.Image.FileName;

        var newProduct = new Product
        {
            Name = product.Name,
            Price = product.Price,
            ImageUrl = product.ImageUrl,
            CategoryId = product.CategoryId,
            Made = product.Made
        };

        foreach (var item in product.SelectedColours)
        {
            newProduct.ProductColours.Add(new ProductColor()
            {
                ColorId = item
            });
        }
        foreach (var item in product.SelectedSizes)
        {
            newProduct.ProductSizes.Add(new ProductSize()
            {
                SizeId = item
            });
        }

        var productResult = await _unitOfWork.ProductService.CreateProduct(newProduct);
        if (productResult == "Exist")
        {
            return View(product);
        }
        _unitOfWork.Save();
        return RedirectToAction("GetProducts");

    }

    [HttpGet]
    public async Task<IActionResult> EditProduct(int id)
    {
        var product = await _unitOfWork.ProductService.GetProduct(id);
        var colours = await _unitOfWork.ColoreService.GetColores();
        var selectColours = product.ProductColours.Select(x => new Colore()
        {
            Id = x.Colore!.Id,
            Code = x.Colore.Code
        });

        var selectColoursList = new List<SelectListItem>();
        colours.ForEach(i => selectColoursList.Add(new SelectListItem(
            i.Code, i.Id.ToString(),
            selectColours
            .Select(x => x.Id)
            .Contains(i.Id)
            )));

        var sizes = await _unitOfWork.SizeService.GetSizes();
        var selectSizes = product.ProductSizes.Select(x => new Size()
        {
            Id = x.Size!.Id,
            Name = x.Size.Name
        });

        var selectSizesList = new List<SelectListItem>();
        colours.ForEach(i => selectSizesList.Add(new SelectListItem(
            i.Name, i.Id.ToString(),
            selectSizes
            .Select(x => x.Id)
            .Contains(i.Id)
            )));

        var newProduct = new ProductPostViewModel()
        {
            Id = product.Id.ToString(),
            Name = product.Name,
            Price = product.Price,
            Made = product.Made,
            CategoryId = product.CategoryId,
            ImageUrl = product.ImageUrl,
            Colours = selectColoursList,
            Sizes = selectSizesList,

        };

        ViewData["ColoreId"] = new SelectList(_dbContext.Colors, "Id", "Name");
        ViewData["SizeId"] = new SelectList(_dbContext.Sizes, "Id", "Name");
        ViewData["CategoryId"] = new SelectList(_dbContext.Categories, "Id", "Name");
        return View(newProduct);
    }

    [HttpPost]
    public async Task<IActionResult> EditProduct(ProductPostViewModel product)
    {
        var oldProduct = await _unitOfWork.ProductService.GetProduct(int.Parse(product.Id!));
        if (product.Image != null)
        {
            string ImageFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
            string imagepath = Path.Combine(ImageFolder, product!.Image!.FileName);


            var image = oldProduct.ImageUrl;
            var ImageOldPath = Path.Combine(ImageFolder, image);
            product.ImageUrl = product.Image.FileName;

            if (imagepath != ImageOldPath)
            {
                // Delete Old File
                System.IO.File.Delete(ImageOldPath);

                // Save New File
                product.Image.CopyTo(new FileStream(imagepath, FileMode.Create));
            }

            oldProduct.ImageUrl = product.ImageUrl;
        }

        oldProduct.Name = product.Name;
        oldProduct.Price = product.Price;

        oldProduct.CategoryId = product.CategoryId;
        oldProduct.Made = product.Made;

        var selectedColours = product.SelectedColours;
        var existingColours = oldProduct.ProductColours.Select(x => x.ColorId).ToList();

        var toAdd = selectedColours.Except(existingColours).ToList();
        var toRemove = existingColours.Except(selectedColours).ToList();

        oldProduct.ProductColours = oldProduct.ProductColours.Where(x => !toRemove.Contains(x.ColorId)).ToList();


        foreach (var item in toAdd)
        {
            oldProduct.ProductColours.Add(new ProductColor()
            {
                ColorId = item,
                ProductId = oldProduct.Id
            });
        }


        var selectedSizes = product.SelectedSizes;
        var existingSizes = oldProduct.ProductSizes.Select(x => x.SizeId).ToList();

        var toAddSize = selectedSizes.Except(existingSizes).ToList();
        var toRemoveSize = existingSizes.Except(selectedSizes).ToList();

        oldProduct.ProductSizes = oldProduct.ProductSizes.Where(x => !toRemoveSize.Contains(x.SizeId)).ToList();


        foreach (var item in toAddSize)
        {
            oldProduct.ProductSizes.Add(new ProductSize()
            {
                SizeId = item,
                ProductId = oldProduct.Id
            });
        }



        await _unitOfWork.ProductService.CreateProduct(oldProduct);

        _unitOfWork.Save();
        return RedirectToAction("GetProducts");

    }

    [HttpGet]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var product = await _unitOfWork.ProductService.GetProduct(id);
        return View(product);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteProduct(Product product)
    {
        string ImageFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");

        var oldProduct = await _unitOfWork.ProductService.GetProduct(product.Id);
        var image = oldProduct.ImageUrl;
        var ImageOldPath = Path.Combine(ImageFolder, image);

        // Delete Old File
        System.IO.File.Delete(ImageOldPath);



        await _unitOfWork.ProductService.DeleteProduct(product);

        _unitOfWork.Save();

        return RedirectToAction("GetProducts");
    }
}