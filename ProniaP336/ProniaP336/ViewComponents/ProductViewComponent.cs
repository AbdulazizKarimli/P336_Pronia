using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProniaP336.Contexts;

namespace ProniaP336.ViewComponents;

public class ProductViewComponent : ViewComponent
{
    private readonly PronioDbContext _context;

    public ProductViewComponent(PronioDbContext context)
    {
        _context = context;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var products = await _context.Products.Where(p => !p.IsDeleted).Take(8).ToListAsync();

        return View(products);
    }
}