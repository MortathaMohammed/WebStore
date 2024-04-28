using Fantasia.DataAccess.Entity;
using Fantasia.DataAccess.Service.IService;
using Fantasia.Mvc.Models;
using Microsoft.AspNetCore.Mvc;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;


namespace Fantasia.Mvc.Controllers;
public class ColorController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHostingEnvironment _hostingEnvironment;


    public ColorController(IUnitOfWork unitOfWork, IHostingEnvironment hostingEnvironment)
    {
        _unitOfWork = unitOfWork;
        _hostingEnvironment = hostingEnvironment;

    }
    [HttpGet]
    public async Task<IActionResult> GetColors()
    {
        var colorList = new ColorListColor();
        colorList.Colours = await _unitOfWork.ColorService.GetColours();
        colorList.Color = new Color();
        return View(colorList);
    }

    [HttpGet]
    public async Task<IActionResult> GetColorById(int id)
    {
        var color = await _unitOfWork.ColorService.GetColor(id);
        return View(color);
    }

    [HttpGet]
    public async Task<IActionResult> CreateColor()
    {
        return View();
    }

    public async Task<IActionResult> CreateColore(Color color)
    {
        if (color.Name != null || color.Code != null)
        {

            var newColor = new Color
            {
                Name = color.Name,
                Code = color.Code
            };

            var colorResult = await _unitOfWork.ColorService.CreateColor(newColor);
            if (colorResult == "Exists")
            {
                return View(color);
            }
            _unitOfWork.Save();
            return RedirectToAction("GetColores");
        }
        return View(color);

    }

    [HttpGet]
    public async Task<IActionResult> EditColore(int id)
    {
        var color = await _unitOfWork.ColorService.GetColor(id);
        if (color == null)
        {
            return RedirectToAction("GetColores");
        }

        return View(color);
    }

    [HttpPost]
    public async Task<IActionResult> EditColore(Color color)
    {
        var oldColor = await _unitOfWork.ColorService.GetColor(color.Id);
        oldColor.Name = color.Name;
        oldColor.Code = color.Code;

        await _unitOfWork.ColorService.EditColor(oldColor);
        _unitOfWork.Save();
        return RedirectToAction("GetColores");

    }

    [HttpGet]
    public async Task<IActionResult> DeleteColore(int id)
    {
        var color = await _unitOfWork.ColorService.GetColor(id);
        return View(color);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteColor(Color color)
    {
        var oldColor = await _unitOfWork.ColorService.GetColor(color.Id);
        await _unitOfWork.ColorService.DeleteColor(oldColor);

        _unitOfWork.Save();

        return RedirectToAction("GetColores");
    }
}