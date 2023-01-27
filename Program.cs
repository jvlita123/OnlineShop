using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Sklep_MVC_Projekt.Data;
using Sklep_MVC_Projekt.Repositories;
using Sklep_MVC_Projekt.Services;
using System.ComponentModel.Design;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<DataContext>();

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<DataContext>()
            .AddDefaultUI()
            .AddDefaultTokenProviders();

builder.Services.AddControllersWithViews();


builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<CategoryRepository>();
builder.Services.AddScoped<CustomerProductRepository>();
builder.Services.AddScoped<CustomerProductService>();
builder.Services.AddScoped<CustomerService>();
builder.Services.AddScoped<CustomerRepository>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<OrderRepository>();
builder.Services.AddScoped<PaymentMethodService>();
builder.Services.AddScoped<PaymentMethodRepository>();
builder.Services.AddScoped<ProductOrderRepository>();
builder.Services.AddScoped<ProductOrderService>();
builder.Services.AddScoped<PhotoService>();
builder.Services.AddScoped<PhotoRepository>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<ProductRepository>();
builder.Services.AddScoped<ShippingMethodRepository>();
builder.Services.AddScoped<ShippingMethodService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
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

app.MapRazorPages();

app.Run();
