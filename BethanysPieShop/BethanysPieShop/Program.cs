using System.Text.Json.Serialization;
using BethanysPieShop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args); // applies defaults: looks at appsettings.json, Kestrel, wwwroot folder, IIS integration; no static void main needed with top-level statements

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IPieRepository, PieRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddScoped<IShoppingCart, ShoppingCart>(sp => ShoppingCart.GetCart(sp)); //passes in a service provider and already calls a method

builder.Services.AddSession(); // services that enable session state
builder.Services.AddHttpContextAccessor(); // services that enable access to HttpContext

builder
    .Services.AddControllersWithViews() //framework services that enable MVC; just AddControllers if API only
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles
    ); // prevents infinite loops in JSON serialization

builder.Services.AddRazorPages(); //framework services that enable Razor Pages
builder.Services.AddDbContext<BethanysPieShopContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")) //GetConnectionString is shorthand appsettings.json reference (didn't change?)
); // add dbContext for Entity Framework Core

builder
    .Services.AddDefaultIdentity<IdentityUser>()
    .AddEntityFrameworkStores<BethanysPieShopContext>(); // important scaffold addition for auth, we removed options => options.SignIn.RequireConfirmedAccount = true

var app = builder.Build(); // the instance that allows us to set up middleware

app.UseStaticFiles(); // middleware that serves static files in wwwroot
app.UseSession(); // middleware that enables session state
app.UseAuthentication(); // middleware that enables authentication, with the nuget auth package
app.UseAuthorization(); // middleware for authorization, e.g. [Authorize] attribute

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); // middleware that shows detailed error messages
}

app.MapDefaultControllerRoute(); // sets MVC defaults with {controller=Home}/{action=Index}/{id?} convention mirroring code (instead of MapControllerRoute)

// app.MapControllers would replace the above line if it were an API only project
app.MapRazorPages(); // middleware that sets up Razor Pages by enabling Razor Page model

DbInitializer.Seed(app); // seed the database if there's no data already in it

app.Run();
