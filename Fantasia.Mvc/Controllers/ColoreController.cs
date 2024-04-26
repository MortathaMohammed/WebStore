using Fantasia.DataAccess.Entity;
using Fantasia.DataAccess.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
namespace Fantasia.Mvc.Controllers;
public class ColoreController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHostingEnvironment _hostingEnvironment;

    public ColoreController(IUnitOfWork unitOfWork, IHostingEnvironment hostingEnvironment)
    {
        _unitOfWork = unitOfWork;
        _hostingEnvironment = hostingEnvironment;
    }
    [HttpGet]
    public async Task<IActionResult> GetColores()
    {
        var colores = await _unitOfWork.ColoreService.GetColores();
        return View(colores);
    }

    [HttpGet]
    public async Task<IActionResult> GetProductById(int id)
    {
        var colore = await _unitOfWork.ColoreService.GetColore(id);
        return View(colore);
    }

    [HttpGet]
    public async Task<IActionResult> CreateColore()
    {
        return View();
    }

    public async Task<IActionResult> CreateColore(Colore colore)
    {

        var newColore = new Colore
        {
            Name = colore.Name,
            Code = colore.Code
        };

        var coloreResult = await _unitOfWork.ColoreService.CreateColore(newColore);
        if (coloreResult == "Exist")
        {
            return View(colore);
        }
        _unitOfWork.Save();
        return RedirectToAction("GetColores");

    }

    [HttpGet]
    public async Task<IActionResult> EditColore(int id)
    {
        var colore = await _unitOfWork.ColoreService.GetColore(id);
        if (colore == null)
        {
            return RedirectToAction("GetColores");
        }

        return View(colore);
    }

    [HttpPost]
    public async Task<IActionResult> EditColore(Colore colore)
    {
        var oldColore = await _unitOfWork.ColoreService.GetColore(colore.Id);
        oldColore.Name = colore.Name;
        oldColore.Code = colore.Code;

        await _unitOfWork.ColoreService.CreateColore(oldColore);
        _unitOfWork.Save();
        return RedirectToAction("GetColores");

    }

    [HttpGet]
    public async Task<IActionResult> DeleteColore(int id)
    {
        var colore = await _unitOfWork.ColoreService.GetColore(id);
        return View(colore);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteColore(Colore colore)
    {
        var oldColore = await _unitOfWork.ColoreService.GetColore(colore.Id);
        await _unitOfWork.ColoreService.DeleteColore(oldColore);

        _unitOfWork.Save();

        return RedirectToAction("GetSizes");
    }
}