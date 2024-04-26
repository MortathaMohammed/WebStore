using Fantasia.DataAccess.Entity;
using Fantasia.DataAccess.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
namespace Fantasia.Mvc.Controllers;
public class SizeController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHostingEnvironment _hostingEnvironment;

    public SizeController(IUnitOfWork unitOfWork, IHostingEnvironment hostingEnvironment)
    {
        _unitOfWork = unitOfWork;
        _hostingEnvironment = hostingEnvironment;
    }

    [HttpGet]
    public async Task<IActionResult> GetSize()
    {
        var sizes = await _unitOfWork.SizeService.GetSizes();
        return View(sizes);
    }

    [HttpGet]
    public async Task<IActionResult> GetSizeById(int id)
    {
        var size = await _unitOfWork.SizeService.GetSize(id);
        return View(size);
    }

    [HttpGet]
    public async Task<IActionResult> CreateSize()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateSize(Size size)
    {

        var newSize = new Size
        {
            Name = size.Name,
        };

        var sizeResult = await _unitOfWork.SizeService.CreateSize(newSize);
        if (sizeResult == "Exist")
        {
            return View(size);
        }
        _unitOfWork.Save();
        return RedirectToAction("GetSizes");

    }

    [HttpGet]
    public async Task<IActionResult> EditSize(int id)
    {
        var size = await _unitOfWork.SizeService.GetSize(id);
        if (size == null)
        {
            return RedirectToAction("GetSizes");
        }

        return View(size);
    }

    [HttpPost]
    public async Task<IActionResult> EditSize(Size size)
    {
        var oldSize = await _unitOfWork.SizeService.GetSize(size.Id);
        oldSize.Name = size.Name;


        await _unitOfWork.SizeService.CreateSize(oldSize);
        _unitOfWork.Save();
        return RedirectToAction("GetSizes");

    }

    [HttpGet]
    public async Task<IActionResult> DeleteSize(int id)
    {
        var size = await _unitOfWork.SizeService.GetSize(id);
        return View(size);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteSize(Size size)
    {
        var oldSize = await _unitOfWork.SizeService.GetSize(size.Id);
        await _unitOfWork.SizeService.DeleteSize(oldSize);

        _unitOfWork.Save();

        return RedirectToAction("GetSizes");
    }
}