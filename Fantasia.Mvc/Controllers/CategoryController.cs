using Fantasia.DataAccess.Entity;
using Fantasia.DataAccess.Service.IService;
using Microsoft.AspNetCore.Mvc;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
namespace Fantasia.Mvc.Controllers;
public class CateogryController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHostingEnvironment _hostingEnvironment;

    public CateogryController(IUnitOfWork unitOfWork,
                             IHostingEnvironment hostingEnvironment)
    {
        _unitOfWork = unitOfWork;
        _hostingEnvironment = hostingEnvironment;
    }

    [HttpGet]
    public async Task<IActionResult> GetCategories()
    {
        var categories = await _unitOfWork.CategoryService.GetCategories();
        return View(categories);
    }

    [HttpGet]
    public async Task<IActionResult> GetCategoryById(int id)
    {
        var category = await _unitOfWork.CategoryService.GetCategory(id);
        return View(category);
    }

    [HttpGet]
    public async Task<IActionResult> CreateCategory()
    {
        return View();
    }

    public async Task<IActionResult> CreateCategory(Category category)
    {
        string ImageFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
        string imagepath = Path.Combine(ImageFolder, category.Image.FileName);
        category.Image.CopyTo(new FileStream(imagepath, FileMode.Create));
        category.ImageUrl = category.Image.FileName;

        var newCategory = new Category
        {
            Name = category.Name,

            ImageUrl = category.ImageUrl,

        };

        var categoryResult = await _unitOfWork.CategoryService.CreateCategory(newCategory);
        if (categoryResult == "Exist")
        {
            return View(category);
        }
        _unitOfWork.Save();
        return RedirectToAction("GetCategories");

    }

    [HttpGet]
    public async Task<IActionResult> Editcategory(int id)
    {
        var category = await _unitOfWork.CategoryService.GetCategory(id);
        if (category == null)
        {
            return RedirectToAction("GetCategories");
        }

        return View(category);
    }

    [HttpPost]
    public async Task<IActionResult> EditCategory(Category category)
    {
        string ImageFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
        string imagepath = Path.Combine(ImageFolder, category.Image.FileName);


        var oldCategory = await _unitOfWork.CategoryService.GetCategory(category.Id);
        var image = oldCategory.ImageUrl;
        var ImageOldPath = Path.Combine(ImageFolder, image);
        category.ImageUrl = category.Image.FileName;

        if (imagepath != ImageOldPath)
        {
            // Delete Old File
            System.IO.File.Delete(ImageOldPath);

            // Save New File
            category.Image.CopyTo(new FileStream(imagepath, FileMode.Create));
        }

        oldCategory.Name = category.Name;

        oldCategory.ImageUrl = category.ImageUrl;



        await _unitOfWork.CategoryService.EditCategory(oldCategory);
        _unitOfWork.Save();
        return RedirectToAction("GetCategories");

    }

    [HttpGet]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        var category = await _unitOfWork.CategoryService.GetCategory(id);
        return View(category);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteCategory(Category category)
    {
        string ImageFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");

        var oldCategory = await _unitOfWork.CategoryService.GetCategory(category.Id);
        var image = oldCategory.ImageUrl;
        var ImageOldPath = Path.Combine(ImageFolder, image);

        // Delete Old File
        System.IO.File.Delete(ImageOldPath);



        await _unitOfWork.CategoryService.DeleteCategory(oldCategory);

        _unitOfWork.Save();

        return RedirectToAction("GetCateogies");
    }
}