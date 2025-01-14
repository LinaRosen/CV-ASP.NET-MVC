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

// L�gg till DbContext
builder.Services.AddDbContext<TestDataContext>(opt =>
    opt.UseLazyLoadingProxies(false).UseSqlServer(builder.Configuration.GetConnectionString("TestDataContext")));

// L�gg till Identity-tj�nster f�r autentisering
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

builder.Logging.AddConsole(); // L�gg till konsollogging (vilket inneb�r att loggar skrivs till konsolen)




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

// L�gg till autentisering f�re auktorisering
app.UseAuthentication();  // Den h�r raden g�r autentisering
app.UseAuthorization();   // Den h�r raden g�r auktorisering

// Definiera rutten f�r controller och action
app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();


