using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProniaP336.Helpers;
using ProniaP336.Helpers.Enums;
using ProniaP336.Models;

namespace ProniaP336.Controllers;

public class UserController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IConfiguration _configuration;

    public UserController(UserManager<AppUser> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
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

        string token = await _userManager.GenerateEmailConfirmationTokenAsync(appUser);

        string link = Url.Action("ConfirmEmail", "Auth", new { email = appUser.Email, token }, HttpContext.Request.Scheme, HttpContext.Request.Host.Value);

        string body = $"<a href='{link}'>Confirm your email</a>";

        EmailHelper emailHelper = new EmailHelper(_configuration);
        await emailHelper.SendEmailAsync(new MailRequest { ToEmail = appUser.Email, Subject = "Confirm Email", Body = body });

        await _userManager.AddToRoleAsync(appUser, Roles.User.ToString());

        return RedirectToAction("Index", "Home");
    }
}