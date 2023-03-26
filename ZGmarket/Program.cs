using Microsoft.AspNetCore.Authentication.Cookies;
using MySqlConnector;
using ZGmarket.Models.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient(x =>
  new MySqlConnection(builder.Configuration.GetConnectionString("MySql")));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).
    AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, (conf) => conf.LoginPath = "/Account/Login");
builder.Services.AddAuthorization();


builder.Services.AddScoped<NomTypeRepository, NomTypeRepository>();
builder.Services.AddScoped<EmpRepository, EmpRepository>();
builder.Services.AddScoped<NomRepository, NomRepository>();
builder.Services.AddScoped<StockRepository, StockRepository>();
builder.Services.AddScoped<NomStockRepository, NomStockRepository>();
builder.Services.AddScoped<SupplyRepository, SupplyRepository>();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();


//Изминить типы на сложные int => NomType