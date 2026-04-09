using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using safonenko.Data;

namespace safonenko.Controllers;

public class StockMovementsController(WarehouseContext db) : Controller
{
    [ResponseCache(CacheProfileName = "WarehouseCache")]
    public async Task<IActionResult> Index()
    {
        var movements = await db.StockMovements
            .AsNoTracking()
            .Include(m => m.Product)
            .Include(m => m.Supplier)
            .Include(m => m.StorageLocation)
            .OrderByDescending(m => m.Date)
            .ToListAsync();

        return View(movements);
    }

    public async Task<IActionResult> Details(int id)
    {
        var movement = await db.StockMovements
            .AsNoTracking()
            .Include(m => m.Product)
            .Include(m => m.Supplier)
            .Include(m => m.StorageLocation)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (movement is null)
        {
            return NotFound();
        }

        return View(movement);
    }
}
