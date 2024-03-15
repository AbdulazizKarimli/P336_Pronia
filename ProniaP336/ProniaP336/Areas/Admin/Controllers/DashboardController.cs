using Microsoft.AspNetCore.Mvc;

namespace ProniaP336.Areas.Admin.Controllers;

[Area("Admin")]
public class DashboardController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}