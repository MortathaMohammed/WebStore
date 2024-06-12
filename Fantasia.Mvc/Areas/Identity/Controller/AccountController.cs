using Fantasia.DataAccess.Entity.Account;
using Fantasia.Mvc.Helpers;
using Fantasia.Mvc.Models.ViewModel.AuthViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace Fantasia.Mvc.Areas.Identity.Controllers;
[Area("Identity")]
public class AccountsController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AccountsController(UserManager<AppUser> userManager,
        RoleManager<IdentityRole> roleManager, SignInManager<AppUser> signInManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
    }

    public IActionResult Login()
    {
        return View();
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home", new { area = "Customer" });
            }
            ModelState.AddModelError(string.Empty, "Invalid login attempt");
        }
        return View(model);
    }

    public IActionResult Register()
    {
        return View();
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = new AppUser
            {
                UserName = model.Email,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                if (!await _roleManager.RoleExistsAsync(model.RoleName))
                {
                    await _roleManager.CreateAsync(new IdentityRole(model.RoleName));
                }
                await _userManager.AddToRoleAsync(user, model.RoleName);

                if (!User.IsInRole(StaticData.role_admin))
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                }
                else
                {
                    TempData["newAdminSignUp"] = user.UserName;
                }
                return RedirectToAction("Index", "Home", new { area = "Customer" });
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home", new { area = "Customer" });
    }
}