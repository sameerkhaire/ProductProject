using BAL;
using DAL.Data;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;
using Repository;
using System.Reflection;
using MediatR;
using AutoMapper;
using ShoppingCart.Iservices;
using ShoppingCart.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddControllers();
builder.Services.AddDbContext<ProductDbContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("Defaultcon")));
builder.Services.AddScoped<IProductservices, ProductServices>();
builder.Services.AddHttpClient("products", x => x.BaseAddress = new Uri(builder.Configuration["ServiceUrls:ProductAPI"]));
builder.Services.AddHttpClient("couponss", x => x.BaseAddress = new Uri(builder.Configuration["ServiceUrls:CouponAPI"]));
builder.Services.AddScoped<IBaseRepo<CartHeader>, BaseRepo<CartHeader>>();
builder.Services.AddScoped<IBaseRepo<CartDetails>, BaseRepo<CartDetails>>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();
app.Run();
