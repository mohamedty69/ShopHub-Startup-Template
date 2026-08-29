using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using myshop.BLL.DTOs.Order;
using myshop.BLL.IServices;
using myshop.BLL.Mapping.OrderMapping.OutgoingData;
using myshop.BLL.Mapping.ProductMapping.IncomingData;
using myshop.BLL.Mapping.ProductMapping.IncomingData.Admin;
using myshop.BLL.Mapping.ProductMapping.OutgoingData.Admin;
using myshop.BLL.Mapping.ProductMapping.OutgoingData.Customer;
using myshop.BLL.Mapping.UserMapping.IncomingData;
using myshop.BLL.Mapping.UserMapping.OutgoingData;
using myshop.BLL.Services;
using myshop.DAL.Data;
using myshop.DAL.Iconfiguration;
using myshop.DAL.IRepository;
using myshop.DAL.Repository;
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
typeof(RegisterMapping),typeof(DisplayUserMapping),
typeof(CreateProductProfile),typeof(DisplayProductProfile),
typeof(EditProductProfile),typeof(UpdateProductProfile)
,typeof(DisplayProductForCustomerProfile),typeof(CreateProductProfile),
typeof(SummaryOrderProfile),typeof(OrderItemsProfile));
builder.Services.AddScoped<IFileService, myshop.BLL.Services.FileService>();
builder.Services.AddScoped<IUserService, UserServices>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, myshop.BLL.Services.ProductService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IOrderHeaderRepo, OrderHeaderRepo>();
builder.Services.AddScoped<IOrderDetailRepo, OrderDetailRepo>();

builder.Services.AddHttpContextAccessor();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Home/Login";

    options.AccessDeniedPath = "/Home/AccessDenied";
});
builder.Services.AddMemoryCache();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(opt =>
  { 
      opt.IdleTimeout = TimeSpan.FromMinutes(30);
      opt.Cookie.HttpOnly = true;
      opt.Cookie.IsEssential = true;
  }
);
builder.Services.AddSession();
var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var role = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    string[] roleNames = { "Admin", "Customer" };
    foreach(var roleName in roleNames)
    {
        if (await role.RoleExistsAsync(roleName))
            continue;
        else await role.CreateAsync(new IdentityRole(roleName));
    }
}
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

