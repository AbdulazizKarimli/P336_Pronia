using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProniaP336.Contexts;
using ProniaP336.Models;

namespace ProniaP336.Controllers;

public class ShopController : Controller
{
    private readonly PronioDbContext _context;

    public ShopController(PronioDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        int productCount = await _context.Products.Where(p => !p.IsDeleted).CountAsync();
        ViewBag.ProductCount = productCount;

        return View();
    }

    public async Task<IActionResult> LoadMore(int skip)
    {
        int productCount = await _context.Products.Where(p => !p.IsDeleted).CountAsync();
        if (skip >= productCount)
            return BadRequest();


        List<Product> products = await _context.Products.Where(p => !p.IsDeleted)
            .Skip(skip).Take(8).ToListAsync();

        return PartialView("_ProductPartial", products);
    }

    public async Task<IActionResult> ProductDetail(int id)
    {
        var product = await _context.Products.Include(p => p.Category).Include(p => p.ProductTags).ThenInclude(pt => pt.Tag).FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);

        return PartialView("_ProductModalPartial", product);
    }
}
