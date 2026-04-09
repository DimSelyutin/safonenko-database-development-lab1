using Microsoft.EntityFrameworkCore;
using safonenko.Data;
using safonenko.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews(options =>
{
    options.CacheProfiles.Add("WarehouseCache", new Microsoft.AspNetCore.Mvc.CacheProfile
    {
        Duration = 268,
        Location = Microsoft.AspNetCore.Mvc.ResponseCacheLocation.Any,
        NoStore = false
    });
});

builder.Services.AddDbContext<WarehouseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddResponseCaching();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseResponseCaching();
app.UseMiddleware<DatabaseSeedMiddleware>();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
