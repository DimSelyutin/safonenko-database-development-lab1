using Microsoft.EntityFrameworkCore;
using safonenko.Data;
using safonenko.Models;

namespace safonenko.Middleware;

public class DatabaseSeedMiddleware(RequestDelegate next)
{
    private static bool _seedCompleted;
    private static readonly object SeedLock = new();

    public async Task InvokeAsync(HttpContext context, WarehouseContext db)
    {
        if (!_seedCompleted)
        {
            await EnsureSeededAsync(db);
        }

        await next(context);
    }

    private static async Task EnsureSeededAsync(WarehouseContext db)
    {
        lock (SeedLock)
        {
            if (_seedCompleted)
            {
                return;
            }
        }

        await db.Database.MigrateAsync();

        if (!await db.Categories.AnyAsync())
        {
            db.Categories.AddRange(
                new Category { Name = "Сухие смеси" },
                new Category { Name = "Инструменты" },
                new Category { Name = "Крепеж" });
            await db.SaveChangesAsync();
        }

        if (!await db.StorageLocations.AnyAsync())
        {
            db.StorageLocations.AddRange(
                new StorageLocation { Row = "A", Shelf = "1", Cell = "01" },
                new StorageLocation { Row = "A", Shelf = "1", Cell = "02" },
                new StorageLocation { Row = "B", Shelf = "3", Cell = "11" });
            await db.SaveChangesAsync();
        }

        if (!await db.Suppliers.AnyAsync())
        {
            db.Suppliers.AddRange(
                new Supplier { Name = "ООО СтройПоставка", Details = "ИНН 770000001, Москва" },
                new Supplier { Name = "ИП Инструмент-Сервис", Details = "ИНН 770000002, Тула" },
                new Supplier { Name = "ЗАО КрепМаркет", Details = "ИНН 770000003, Санкт-Петербург" });
            await db.SaveChangesAsync();
        }

        if (!await db.Products.AnyAsync())
        {
            var categories = await db.Categories.ToListAsync();

            db.Products.AddRange(
                new Product
                {
                    Article = "SM-001",
                    Name = "Штукатурка гипсовая",
                    Unit = "мешок",
                    Weight = 25,
                    MinStock = 20,
                    CategoryId = categories.Single(c => c.Name == "Сухие смеси").Id
                },
                new Product
                {
                    Article = "IN-017",
                    Name = "Шпатель 250 мм",
                    Unit = "шт",
                    Weight = 0.35m,
                    MinStock = 15,
                    CategoryId = categories.Single(c => c.Name == "Инструменты").Id
                },
                new Product
                {
                    Article = "KR-120",
                    Name = "Саморез 4.2x75",
                    Unit = "уп",
                    Weight = 1.2m,
                    MinStock = 30,
                    CategoryId = categories.Single(c => c.Name == "Крепеж").Id
                });
            await db.SaveChangesAsync();
        }

        if (!await db.StockMovements.AnyAsync())
        {
            var products = await db.Products.ToListAsync();
            var suppliers = await db.Suppliers.ToListAsync();
            var locations = await db.StorageLocations.ToListAsync();

            db.StockMovements.AddRange(
                new StockMovement
                {
                    Date = DateTime.UtcNow.AddDays(-5),
                    OperationType = StockMovementType.Income,
                    Quantity = 60,
                    DocumentBase = "Накладная №101",
                    ProductId = products.Single(p => p.Article == "SM-001").Id,
                    SupplierId = suppliers.First().Id,
                    StorageLocationId = locations.First().Id
                },
                new StockMovement
                {
                    Date = DateTime.UtcNow.AddDays(-3),
                    OperationType = StockMovementType.Outcome,
                    Quantity = 44,
                    DocumentBase = "Требование №17",
                    ProductId = products.Single(p => p.Article == "SM-001").Id,
                    SupplierId = suppliers.First().Id,
                    StorageLocationId = locations.First().Id
                },
                new StockMovement
                {
                    Date = DateTime.UtcNow.AddDays(-2),
                    OperationType = StockMovementType.Income,
                    Quantity = 18,
                    DocumentBase = "Накладная №220",
                    ProductId = products.Single(p => p.Article == "IN-017").Id,
                    SupplierId = suppliers[1].Id,
                    StorageLocationId = locations[1].Id
                },
                new StockMovement
                {
                    Date = DateTime.UtcNow.AddDays(-1),
                    OperationType = StockMovementType.Income,
                    Quantity = 25,
                    DocumentBase = "Накладная №335",
                    ProductId = products.Single(p => p.Article == "KR-120").Id,
                    SupplierId = suppliers[2].Id,
                    StorageLocationId = locations[2].Id
                });
            await db.SaveChangesAsync();
        }

        lock (SeedLock)
        {
            _seedCompleted = true;
        }
    }
}
