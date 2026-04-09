using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using safonenko.Data;

namespace safonenko.Controllers;

public class CategoriesController(WarehouseContext db) : Controller
{
    [ResponseCache(CacheProfileName = "WarehouseCache")]
    public async Task<IActionResult> Index()
    {
        var categories = await db.Categories
            .AsNoTracking()
            .OrderBy(c => c.Name)
            .ToListAsync();

        return View(categories);
    }

    public async Task<IActionResult> Details(int id)
    {
        var category = await db.Categories
            .AsNoTracking()
            .Include(c => c.Products)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (category is null)
        {
            return NotFound();
        }

        return View(category);
    }
}
