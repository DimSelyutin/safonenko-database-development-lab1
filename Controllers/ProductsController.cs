using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using safonenko.Data;
using safonenko.Models;
using safonenko.ViewModels;

namespace safonenko.Controllers;

public class ProductsController(WarehouseContext db) : Controller
{
    [ResponseCache(CacheProfileName = "WarehouseCache")]
    public async Task<IActionResult> Index(string? search)
    {
        var query = db.Products
            .AsNoTracking()
            .Include(p => p.Category)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            search = search.Trim();
            query = query.Where(p => p.Article.Contains(search) || p.Name.Contains(search));
        }

        ViewBag.Search = search;
        var products = await query.OrderBy(p => p.Name).ToListAsync();
        return View(products);
    }

    public async Task<IActionResult> Details(int id)
    {
        var product = await db.Products
            .AsNoTracking()
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (product is null)
        {
            return NotFound();
        }

        return View(product);
    }

    [ResponseCache(CacheProfileName = "WarehouseCache")]
    public async Task<IActionResult> Balances()
    {
        var movements = db.StockMovements.AsNoTracking();
        var products = db.Products.AsNoTracking().Include(p => p.Category);

        var balance = await products
            .Select(p => new StockBalanceViewModel
            {
                ProductId = p.Id,
                Article = p.Article,
                ProductName = p.Name,
                CategoryName = p.Category != null ? p.Category.Name : string.Empty,
                MinStock = p.MinStock,
                CurrentStock = movements
                    .Where(m => m.ProductId == p.Id)
                    .Select(m => m.OperationType == StockMovementType.Income ? m.Quantity : -m.Quantity)
                    .DefaultIfEmpty(0)
                    .Sum()
            })
            .OrderBy(v => v.ProductName)
            .ToListAsync();

        return View(balance);
    }

    [ResponseCache(CacheProfileName = "WarehouseCache")]
    public async Task<IActionResult> ReorderList()
    {
        var balance = await GetBalancesAsync();
        var reorder = balance.Where(x => x.CurrentStock < x.MinStock).ToList();
        return View(reorder);
    }

    private async Task<List<StockBalanceViewModel>> GetBalancesAsync()
    {
        var movements = db.StockMovements.AsNoTracking();

        return await db.Products
            .AsNoTracking()
            .Include(p => p.Category)
            .Select(p => new StockBalanceViewModel
            {
                ProductId = p.Id,
                Article = p.Article,
                ProductName = p.Name,
                CategoryName = p.Category != null ? p.Category.Name : string.Empty,
                MinStock = p.MinStock,
                CurrentStock = movements
                    .Where(m => m.ProductId == p.Id)
                    .Select(m => m.OperationType == StockMovementType.Income ? m.Quantity : -m.Quantity)
                    .DefaultIfEmpty(0)
                    .Sum()
            })
            .OrderBy(x => x.ProductName)
            .ToListAsync();
    }
}
