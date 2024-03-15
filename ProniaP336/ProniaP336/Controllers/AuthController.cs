using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProniaP336.Models;

namespace ProniaP336.Controllers;

public class AuthController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;

    public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public IActionResult Login()
    {
        if (User.Identity.IsAuthenticated)
            return RedirectToAction("Index", "Home");

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel loginViewModel)
    {
        if (User.Identity.IsAuthenticated)
            return RedirectToAction("Index", "Home");

        if (!ModelState.IsValid)
            return View();

        var user = await _userManager.FindByNameAsync(loginViewModel.UsernameOrEmail);
        if (user == null)
        {
            user = await _userManager.FindByEmailAsync(loginViewModel.UsernameOrEmail);
            if (user == null)
            {
                ModelState.AddModelError("", "Username/email or password incorrect");
                return View();
            }
        }

        var signInResult = await _signInManager.PasswordSignInAsync(user.UserName, loginViewModel.Password, loginViewModel.RememberMe, false);
        if (!signInResult.Succeeded)
        {
            ModelState.AddModelError("", "Username/email or password incorrect");
            return View();
        }

        return RedirectToAction("Index", "Home");
    }

    public async Task<IActionResult> Logout()
    {
        if (!User.Identity.IsAuthenticated)
            return BadRequest();

        await _signInManager.SignOutAsync();

        return RedirectToAction("Index", "Home");
    }
}