using BethanysPieShop.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args); // applies defaults: looks at appsettings.json, Kestrel, wwwroot folder, IIS integration; no static void main needed with top-level statements

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IPieRepository, PieRepository>();

builder.Services.AddControllersWithViews(); //framework services that enable MVC
builder.Services.AddDbContext<BethanysPieShopContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")) //GetConnectionString is shorthand appsettings.json reference
); // add dbContext for Entity Framework Core

var app = builder.Build(); // the instance that allows us to set up middleware

app.UseStaticFiles(); // middleware that serves static files in wwwroot

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); // middleware that shows detailed error messages
}

app.MapDefaultControllerRoute(); // sets MVC defaults with {controller=Home}/{action=Index}/{id?} convention mirroring code (instead of MapControllerRoute)
DbInitializer.Seed(app); // seed the database if there's no data already in it

app.Run();