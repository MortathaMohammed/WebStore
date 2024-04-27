using Fantasia.DataAccess.Data;
using Fantasia.DataAccess.Entity;
using Fantasia.DataAccess.Service.IService;
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
        return View();
    }

    public async Task<IActionResult> CreateProduct(Product product)
    {
        string ImageFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
        string imagepath = Path.Combine(ImageFolder, product.Image.FileName);
        product.Image.CopyTo(new FileStream(imagepath, FileMode.Create));
        product.ImageUrl = product.Image.FileName;

        var newProduct = new Product
        {
            Name = product.Name,
            Price = product.Price,
            SizeId = product.SizeId,
            ColoreId = product.ColoreId,
            ImageUrl = product.ImageUrl,
            CategoryId = product.CategoryId,
            Made = product.Made
        };

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
        if (product == null)
        {
            return RedirectToAction("GetProducts");
        }
        ViewData["ColoreId"] = new SelectList(_dbContext.Colors, "Id", "Name");
        ViewData["SizeId"] = new SelectList(_dbContext.Sizes, "Id", "Name");
        ViewData["CategoryId"] = new SelectList(_dbContext.Categories, "Id", "Name");
        return View(product);
    }

    [HttpPost]
    public async Task<IActionResult> EditProduct(Product product)
    {
        string ImageFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
        string imagepath = Path.Combine(ImageFolder, product.Image.FileName);


        var oldProduct = await _unitOfWork.ProductService.GetProduct(product.Id);
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

        oldProduct.Name = product.Name;
        oldProduct.Price = product.Price;
        oldProduct.SizeId = product.SizeId;
        oldProduct.ColoreId = product.ColoreId;
        oldProduct.ImageUrl = product.ImageUrl;
        oldProduct.CategoryId = product.CategoryId;
        oldProduct.Made = product.Made;


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