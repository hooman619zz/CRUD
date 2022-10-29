using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CrudTest.Data;
using CrudTest.Repository;
using CrudTest.Models;
using Autofac;
using Autofac.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();
DependencyInjection.AddRepository(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}




app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
