using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProniaP336.Areas.Admin.ViewModels.TagViewModels;
using ProniaP336.Contexts;
using ProniaP336.Models;

namespace ProniaP336.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class TagController : Controller
{
    private readonly PronioDbContext _context;

    public TagController(PronioDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var tags = await _context.Tags.OrderByDescending(t => t.Id).ToListAsync();

        return View(tags);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateTagViewModel createTagViewModel)
    {
        bool isExist = await _context.Tags.AnyAsync(t => t.Name.ToLower() == createTagViewModel.Name.ToLower());
        if (isExist)
        {
            ModelState.AddModelError("Name", "Bele bir tag movcuddur!!!");
            return View();
        }

        await _context.Tags.AddAsync(new Tag()
        {
            Name = createTagViewModel.Name,
        });
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
}
