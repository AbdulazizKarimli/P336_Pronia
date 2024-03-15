using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProniaP336.Contexts;
using ProniaP336.Models;

namespace ProniaP336.Areas.Admin.Controllers;

[Area("Admin")]
public class SliderController : Controller
{
    private readonly PronioDbContext _context;

    public SliderController(PronioDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var sliders = await _context.Sliders.AsNoTracking().ToListAsync();

        return View(sliders);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Slider slider)
    {
        await _context.Sliders.AddAsync(slider);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Detail(int id)
    {
        var slider = await _context.Sliders.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);
        if (slider == null)
            return NotFound();

        return View(slider);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var slider = await _context.Sliders.FirstOrDefaultAsync(s => s.Id == id);
        if (slider == null)
            return NotFound();

        return View(slider);
    }

    [HttpPost]
    [ActionName(nameof(Delete))]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteSlider(int id)
    {
        var slider = await _context.Sliders.FirstOrDefaultAsync(s => s.Id == id);
        if (slider == null)
            return NotFound();

        _context.Sliders.Remove(slider);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Update(int id)
    {
        var slider = await _context.Sliders.FirstOrDefaultAsync(s => s.Id == id);
        if (slider == null)
            return NotFound();

        return View(slider);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(int id, Slider slider)
    {
        var dbSlider = await _context.Sliders.FirstOrDefaultAsync(s => s.Id == id);
        if (dbSlider == null)
            return NotFound();

        dbSlider.Title = slider.Title;
        dbSlider.Description = slider.Description;
        dbSlider.Offer = slider.Offer;
        dbSlider.Image = slider.Image;

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
}