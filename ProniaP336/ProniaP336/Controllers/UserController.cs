using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProniaP336.Helpers;
using ProniaP336.Helpers.Enums;
using ProniaP336.Models;

namespace ProniaP336.Controllers;

public class UserController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IConfiguration _configuration;

    public UserController(UserManager<AppUser> userManager, IConfiguration configuration, SignInManager<AppUser> signInManager)
    {
        _userManager = userManager;
        _configuration = configuration;
        _signInManager = signInManager;
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

    [Authorize]
    public async Task<IActionResult> Profile()
    {
        TempData["Tab"] = "account-dashboard";

        var user = await _userManager.FindByNameAsync(User.Identity.Name);
        if (user == null)
            return NotFound();

        UserUpdateViewModel userUpdateViewModel = new()
        {
            Fullname = user.Fullname,
            UserName = user.UserName,
            Email = user.Email
        };

        UserProfileViewModel userProfileViewModel = new()
        {
            UserUpdateViewModel = userUpdateViewModel
        };

        return View(userProfileViewModel);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> UpdateProfile(UserUpdateViewModel userUpdateProfileViewModel)
    {
        TempData["Tab"] = "account-details";

        UserProfileViewModel userProfileViewModel = new()
        {
            UserUpdateViewModel = userUpdateProfileViewModel
        };

        if (!ModelState.IsValid)
            return View(nameof(Profile), userProfileViewModel);

        var user = await _userManager.FindByNameAsync(User.Identity.Name);
        if (user == null)
            return NotFound();


        if(user.UserName != userUpdateProfileViewModel.UserName && _userManager.Users.Any(u => u.UserName == userUpdateProfileViewModel.UserName))
        {
            ModelState.AddModelError("UserName", "Bele username var");
            return View(nameof(Profile), userProfileViewModel);
        }

        if (user.Email != userUpdateProfileViewModel.Email && _userManager.Users.Any(u => u.Email == userUpdateProfileViewModel.Email))
        {
            ModelState.AddModelError("Email", "Bele email var");
            return View(nameof(Profile), userProfileViewModel);
        }

        if(userUpdateProfileViewModel.CurrentPassword != null)
        {
            if(userUpdateProfileViewModel.NewPassword == null)
            {
                ModelState.AddModelError("NewPassword", "Yeni password null ola bilmez");
                return View(nameof(Profile), userProfileViewModel);
            }

            IdentityResult identityResult = await _userManager.ChangePasswordAsync(user, userUpdateProfileViewModel.CurrentPassword, userUpdateProfileViewModel.NewPassword);
            if (!identityResult.Succeeded)
            {
                foreach (var error in identityResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(nameof(Profile), userProfileViewModel);
            }
        }

        user.Fullname = userUpdateProfileViewModel.Fullname;
        user.UserName = userUpdateProfileViewModel.UserName;
        user.Email = userUpdateProfileViewModel.Email;

        IdentityResult result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View(nameof(Profile), userProfileViewModel);
        }

        await _signInManager.RefreshSignInAsync(user);

        TempData["SuccessMessage"] = "Sizin profiliniz ugurla yenilendi";

        return RedirectToAction(nameof(Profile));
    }
}