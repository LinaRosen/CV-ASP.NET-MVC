//using CV_ASP.NET.DataContext;
//using CV_ASP.NET.Models;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;


//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
//builder.Services.AddControllersWithViews();

//builder.Services.AddDbContext<TestDataContext>(opt =>
//opt.UseLazyLoadingProxies(false).UseSqlServer(builder.Configuration.GetConnectionString("TestDataContext")));
//builder.Services.AddIdentity<Anvandare, 
//    IdentityRole>().AddEntityFrameworkStores<TestDataContext>().AddDefaultTokenProviders(
//    );

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Home/Error");
//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();
//}

//app.UseHttpsRedirection();
//app.UseStaticFiles();

//app.UseRouting();

//app.UseAuthorization();

//app.UseAuthentication();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

//app.Run();

using CV_ASP.NET.Controllers;
using CV_ASP.NET.DataContext;
using CV_ASP.NET.Models;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Lägg till DbContext
builder.Services.AddDbContext<TestDataContext>(opt =>
    opt.UseLazyLoadingProxies(false).UseSqlServer(builder.Configuration.GetConnectionString("TestDataContext")));

// Lägg till Identity-tjänster för autentisering
builder.Services.AddIdentity<Anvandare, IdentityRole>()
    .AddEntityFrameworkStores<TestDataContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
});

builder.Services.AddScoped<MeddelandeController>();

builder.Logging.AddConsole(); // Lägg till konsollogging (vilket innebär att loggar skrivs till konsolen)




var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Lägg till autentisering före auktorisering
app.UseAuthentication();  // Den här raden gör autentisering
app.UseAuthorization();   // Den här raden gör auktorisering

// Definiera rutten för controller och action
app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();


