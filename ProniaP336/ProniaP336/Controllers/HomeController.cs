using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProniaP336.Contexts;

namespace ProniaP336.Controllers;

public class HomeController : Controller
{
    private readonly PronioDbContext _context;

    public HomeController(PronioDbContext context)
    {
        _context = context;

    }

    public async Task<IActionResult> Index()
    {
        var sliders = await _context.Sliders.AsNoTracking().ToListAsync();
        var shippings = await _context.Shippings.AsNoTracking().ToListAsync();

        HomeViewModel homeViewModel = new HomeViewModel
        {
            Sliders = sliders,
            Shippings = shippings
        };

        return View(homeViewModel);
    }
}