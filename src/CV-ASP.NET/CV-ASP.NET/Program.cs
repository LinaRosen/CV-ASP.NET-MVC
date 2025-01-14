using CV_ASP.NET.DataContext;
using CV_ASP.NET.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// **Tj�nster m�ste registreras innan `builder.Build()`**
// L�gg till Identity och TestDataContext
builder.Services.AddDbContext<TestDataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TestDataContext")));

builder.Services.AddIdentity<Anvandare, IdentityRole>()
    .AddEntityFrameworkStores<TestDataContext>()
    .AddDefaultTokenProviders();

// Konfigurera Identity-inst�llningar (frivilligt)
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
});

// L�gg till MVC-tj�nster
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

// **Middleware ordning �r viktigt**
app.UseAuthentication();  // Hantera autentisering
app.UseAuthorization();   // Hantera auktorisering

// **Definiera rutten f�r MVC**
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
//// L�gg till TestDataContext till tj�nsteregistreringen
//builder.Services.AddDbContext<TestDataContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//var app = builder.Build();

//// Add services to the container.
//builder.Services.AddControllersWithViews();

//// L�gg till DbContext
//builder.Services.AddDbContext<TestDataContext>(opt =>
//    opt.UseLazyLoadingProxies(false).UseSqlServer(builder.Configuration.GetConnectionString("TestDataContext")));

//// L�gg till Identity-tj�nster f�r autentisering
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

//// L�gg till autentisering f�re auktorisering
//app.UseAuthentication();  // Den h�r raden g�r autentisering
//app.UseAuthorization();   // Den h�r raden g�r auktorisering

//// Definiera rutten f�r controller och action
//app.MapControllerRoute(
//        name: "default",
//        pattern: "{controller=Home}/{action=Index}/{id?}");


//app.Run();


