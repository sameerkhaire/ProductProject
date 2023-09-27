
using BAL;
using DAL.Data;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Repository;
using Repository.Interface;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;
using System.Text;

namespace ProductWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
            builder.Services.AddControllers();
            builder.Services.AddDbContext<ProductDbContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("Defaultcon")));
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:key"]))
                };
            });

            builder.Services.AddScoped<IBaseRepo<Product>, BaseRepo<Product>>();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(o =>
            {
                o.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "Add token",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                o.OperationFilter<SecurityRequirementsOperationFilter>();
            });
            builder.Services.AddCors(p =>
            {
                p.AddPolicy("sam", o =>
                {
                    o.AllowAnyHeader();
                    o.WithOrigins("http://localhost:4200");
                    o.AllowAnyMethod();
                });
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors("sam");

            app.MapControllers();

            app.Run();
        }
    }
}