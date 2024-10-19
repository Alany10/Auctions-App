using AuctionApp.Areas.Identity.Data;
using AuctionApp.Core;
using AuctionApp.Core.Interfaces;
using AuctionApp.Persistence;
using Microsoft.EntityFrameworkCore;
using AuctionApp.Data;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// dependency injection of service into controller
builder.Services.AddScoped<IAuctionService, AuctionService>();

// dependncy injection of persistence into service
builder.Services.AddScoped<IAuctionPersistence, MySqlAuctionPersistence>();

// auctionsdb
builder.Services.AddDbContext<AuctionDbContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("AuctionDbConnection")));

// identity
builder.Services.AddDbContext<AppIdentityDbContext>(options =>    
    options.UseMySQL(builder.Configuration.GetConnectionString("IdentityDbConnection")));
builder.Services.AddDefaultIdentity<AppIdentityUser>(options => 
    options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<AppIdentityDbContext>();

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

