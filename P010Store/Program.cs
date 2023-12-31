﻿

using P010Store.Data;
using P010Store.Data.Abstract;
using P010Store.Data.Concrete;
using P010Store.Service.Abstract;
using P010Store.Service.Concrete;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<DatabaseContext>();


builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddTransient(typeof(IService<>), typeof(Service<>));

builder.Services.AddTransient(typeof(IProductService), typeof(ProductService));

//builder.Services.AddTransient<IProductService,ProductService>(); -- şeklinde de yazım yapılabilir ikiside çalışıyor

builder.Services.AddTransient(typeof(ICategoryService), typeof(CategoryService));

builder.Services.AddSingleton<IHttpContextAccessor,HttpContextAccessor>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(x =>
{
    x.LoginPath = "/Admin/Login";
    x.AccessDeniedPath = "/AccessDenied";
    x.LogoutPath = "/Admin/Login/Logout";
    x.Cookie.Name = "Administrator";
    x.Cookie.MaxAge = TimeSpan.FromDays(1);
});

builder.Services.AddAuthorization(x =>
{
    x.AddPolicy("AdminPolicy",policy => policy.RequireClaim("Role","Admin"));
    x.AddPolicy("UserPolicy",policy => policy.RequireClaim("Role", "User"));



});
    



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
    name: "admin",
    pattern: "{area:exists}/{controller=Main}/{action=Index}/{id?}");


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
