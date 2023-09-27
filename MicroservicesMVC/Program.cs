using MicroservicesMVC.Implementation;
using MicroservicesMVC.Interface;
using MicroservicesMVC.Utility;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient();
builder.Services.AddHttpClient<IproductServices, ProductServices>();
builder.Services.AddHttpClient<ICouponService, CouponServcies>();
SD.CouponAPIBase = builder.Configuration["ServiceUrls:CouponAPI"];
SD.ProductAPIBase = builder.Configuration["ServiceUrls:ProductAPI"];
SD.ShoppingAPIBase = builder.Configuration["ServiceUrls:ShoppingAPI"];
builder.Services.AddScoped<IproductServices, ProductServices>();
builder.Services.AddScoped<ICouponService, CouponServcies>();
builder.Services.AddScoped<IBaseServices, BaseServices>();
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
