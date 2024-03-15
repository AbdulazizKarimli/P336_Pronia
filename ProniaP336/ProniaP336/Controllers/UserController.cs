using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProniaP336.Models;

namespace ProniaP336.Controllers;

public class UserController : Controller
{
    private readonly UserManager<AppUser> _userManager;

    public UserController(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }

        AppUser appUser = new AppUser()
        {
            Fullname = registerViewModel.Fullname,
            UserName = registerViewModel.Username,
            Email = registerViewModel.Email,
            IsActive = true
        };

        IdentityResult identityResult = await _userManager.CreateAsync(appUser, registerViewModel.Password);
        if (!identityResult.Succeeded)
        {
            foreach (var error in identityResult.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View();
        }

        return RedirectToAction("Index", "Home");
    }
}