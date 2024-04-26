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

    public ProductController(IUnitOfWork unitOfWork, IHostingEnvironment hostingEnvironment)
    {
        _unitOfWork = unitOfWork;
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
        IEnumerable<Colore> colores = (IEnumerable<Colore>)_unitOfWork.ColoreService.GetColores();
        IEnumerable<Size> sizes = (IEnumerable<Size>)_unitOfWork.SizeService.GetSizes();
        ViewData["ColoreId"] = new SelectList(colores, "Id", "Name");
        ViewData["SizeId"] = new SelectList(sizes, "Id", "Name");
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
        IEnumerable<Colore> colores = (IEnumerable<Colore>)_unitOfWork.ColoreService.GetColores();
        IEnumerable<Size> sizes = (IEnumerable<Size>)_unitOfWork.SizeService.GetSizes();
        ViewData["ColoreId"] = new SelectList(colores, "Id", "Name");
        ViewData["SizeId"] = new SelectList(sizes, "Id", "Name");
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
    public async Task<IActionResult> DeleteRecipe(Product product)
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