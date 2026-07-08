using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using myshop.BLL.IServices;
using myshop.BLL.Mapping.UserMapping.IncomingData;
using myshop.BLL.Mapping.UserMapping.OutgoingData;
using myshop.BLL.Services;
using myshop.DAL.Data;
using myshop.DAL.Iconfiguration;
using myshop.DataAccess;
using myshop.Entities.Models;
using Stripe;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
    )) ;

builder.Services.AddIdentity<ApplicationUser,IdentityRole>(
    options=>options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromDays(4)
    ).AddDefaultTokenProviders().AddDefaultUI()
    .AddEntityFrameworkStores<ApplicationDbContext>();


builder.Services.AddAutoMapper(cfg => { },
typeof(RegisterMapping),typeof(DisplayUserMapping));
builder.Services.AddScoped<IUserService, UserServices>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddHttpContextAccessor();


builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();
var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
}





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

app.UseSession();

app.MapRazorPages();
//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Login}/{id?}");

app.Run();

