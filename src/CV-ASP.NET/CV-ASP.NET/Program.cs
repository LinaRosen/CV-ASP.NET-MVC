using CV_ASP.NET.DataContext;
using CV_ASP.NET.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// **Tjänster måste registreras innan `builder.Build()`**
// Lägg till Identity och TestDataContext
builder.Services.AddDbContext<TestDataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TestDataContext")));

builder.Services.AddIdentity<Anvandare, IdentityRole>()
    .AddEntityFrameworkStores<TestDataContext>()
    .AddDefaultTokenProviders();

// Konfigurera Identity-inställningar (frivilligt)
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
});

// Lägg till MVC-tjänster
builder.Services.AddControllersWithViews();

var app = builder.Build();

// **Middleware och pipeline konfiguration**
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// **Middleware ordning är viktigt**
app.UseAuthentication();  // Hantera autentisering
app.UseAuthorization();   // Hantera auktorisering

// **Definiera rutten för MVC**
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();


//using CV_ASP.NET.Controllers;
//using CV_ASP.NET.DataContext;
//using CV_ASP.NET.Models;
//using Microsoft.AspNetCore.Components.Routing;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;


//var builder = WebApplication.CreateBuilder(args);
//// Lägg till TestDataContext till tjänsteregistreringen
//builder.Services.AddDbContext<TestDataContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//var app = builder.Build();

//// Add services to the container.
//builder.Services.AddControllersWithViews();

//// Lägg till DbContext
//builder.Services.AddDbContext<TestDataContext>(opt =>
//    opt.UseLazyLoadingProxies(false).UseSqlServer(builder.Configuration.GetConnectionString("TestDataContext")));

//// Lägg till Identity-tjänster för autentisering
//builder.Services.AddIdentity<Anvandare, IdentityRole>()
//    .AddEntityFrameworkStores<TestDataContext>()
//    .AddDefaultTokenProviders();

//builder.Services.Configure<IdentityOptions>(options =>
//{
//    options.Password.RequireDigit = false;
//    options.Password.RequiredLength = 6;
//    options.Password.RequireNonAlphanumeric = false;
//    options.Password.RequireUppercase = false;
//    options.Password.RequireLowercase = false;
//});

//builder.Services.AddScoped<MeddelandeController>();



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

//// Lägg till autentisering före auktorisering
//app.UseAuthentication();  // Den här raden gör autentisering
//app.UseAuthorization();   // Den här raden gör auktorisering

//// Definiera rutten för controller och action
//app.MapControllerRoute(
//        name: "default",
//        pattern: "{controller=Home}/{action=Index}/{id?}");


//app.Run();


