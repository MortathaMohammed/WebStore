using Fantasia.DataAccess.Entity;
using Fantasia.DataAccess.Service.IService;
using Fantasia.Mvc.Models;
using Microsoft.AspNetCore.Mvc;
namespace Fantasia.Mvc.Areas.Admin.Controllers;
public class CategoryController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public CategoryController(IUnitOfWork unitOfWork,
                             IWebHostEnvironment webHostEnvironment)
    {
        _unitOfWork = unitOfWork;
        _webHostEnvironment = webHostEnvironment;
    }

    [HttpGet]
    public async Task<IActionResult> GetCategories(string search)
    {

        var categories = await _unitOfWork.CategoryService.GetCategories();
        if (!String.IsNullOrEmpty(search))
        {
            categories = categories.Where(n => n.Name!.Contains(search)).ToList();
        }
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
        string ImageFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
        string imagepath = Path.Combine(ImageFolder, category!.Image!.FileName);
        category.Image.CopyTo(new FileStream(imagepath, FileMode.Create));
        category.ImageUrl = category.Image.FileName;

        var newCategory = new Category
        {
            Name = category.Name,

            ImageUrl = category.ImageUrl,

        };

        var categoryResult = await _unitOfWork.CategoryService.CreateCategory(newCategory);
        if (categoryResult == "Exists")
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
        var oldCategory = await _unitOfWork.CategoryService.GetCategory(category.Id);
        if (category.Image != null)
        {
            string ImageFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
            string imagepath = Path.Combine(ImageFolder, category!.Image!.FileName);


            var image = oldCategory.ImageUrl;
            var ImageOldPath = Path.Combine(ImageFolder, image!);
            category.ImageUrl = category.Image.FileName;

            if (imagepath != ImageOldPath)
            {
                // Delete Old File
                System.IO.File.Delete(ImageOldPath);

                // Save New File
                category.Image.CopyTo(new FileStream(imagepath, FileMode.Create));
            }

            oldCategory.ImageUrl = category.ImageUrl;
        }


        oldCategory.Name = category.Name;

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
        string ImageFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");

        var oldCategory = await _unitOfWork.CategoryService.GetCategory(category.Id);
        var image = oldCategory.ImageUrl;
        var ImageOldPath = Path.Combine(ImageFolder, image);

        // Delete Old File
        System.IO.File.Delete(ImageOldPath);



        await _unitOfWork.CategoryService.DeleteCategory(oldCategory);

        _unitOfWork.Save();

        return RedirectToAction("GetCategories");
    }
}