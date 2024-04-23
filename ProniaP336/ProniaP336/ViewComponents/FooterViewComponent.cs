﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProniaP336.Contexts;

namespace ProniaP336.ViewComponents;

public class FooterViewComponent : ViewComponent
{
    private readonly PronioDbContext _context;

    public FooterViewComponent(PronioDbContext context)
    {
        _context = context;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var settings = await _context.Settings.ToDictionaryAsync(x => x.Key, x => x.Value);

        return View(settings);
    }
}