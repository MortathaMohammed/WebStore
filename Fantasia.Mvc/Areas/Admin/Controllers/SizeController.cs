using Fantasia.DataAccess.Entity;
using Fantasia.DataAccess.Service.IService;
using Fantasia.Mvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace Fantasia.Mvc.Areas.Admin.Controllers;
public class SizeController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public SizeController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
    {
        _unitOfWork = unitOfWork;
        _webHostEnvironment = webHostEnvironment;
    }

    [HttpGet]
    public async Task<IActionResult> GetSizes()
    {
        var sizeList = new SizeListSize();
        sizeList.Sizes = await _unitOfWork.SizeService.GetSizes();
        sizeList.Size = new Size();
        return View(sizeList);
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
        if (sizeResult == "Exists")
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


        await _unitOfWork.SizeService.EditSize(oldSize);
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