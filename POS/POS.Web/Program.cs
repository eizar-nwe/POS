
using Microsoft.EntityFrameworkCore;
using POS.Domain.DAO;
using POS.Repositories.Domain;
using POS.Services;
using POS.UnitOfWorks;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//get the DB configuration from appsetting.json
var config = builder.Configuration;
//register the DBContext to connect to the database
builder.Services.AddDbContext<POSDbContext>(o => o.UseSqlServer(config.GetConnectionString("POSDB")));
builder.Services.AddRazorPages();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

//depedency injection for all of domains
builder.Services.AddTransient<IStockGroupService, StockGroupService>();
builder.Services.AddTransient<IStockItemService, StockItemService>();
builder.Services.AddTransient<ISupplierServices, SupplierService>();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
