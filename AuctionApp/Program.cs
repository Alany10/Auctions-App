using AuctionApp.Core;
using AuctionApp.Core.Interfaces;
using AuctionApp.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AuctionDbContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("AuctionDbConnection")));

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IAuctionService, AuctionService>();

builder.Services.AddScoped<IAuctionPersistence, MySqlAuctionPersistence>();

builder.Services.AddAutoMapper(typeof(Program));

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

