using Microsoft.AspNetCore.Mvc;

namespace ProniaP336.Controllers;

public class ShopController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
