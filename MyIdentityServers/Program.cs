using Fluent.Infrastructure.FluentModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var migrationsAssembly = typeof(Program).GetTypeInfo().Assembly.GetName().Name;
const string connectionString = @"Data Source=RSPLLT754\MSSQLSERVER01;Initial Catalog=MYIdentityServer;Integrated Security=True;TrustServerCertificate=True";
builder.Services.AddIdentityServer().AddAspNetIdentity<IdentityUser>()
    .AddConfigurationStore(options =>
    {
        options.ConfigureDbContext = b => b.UseSqlServer(connectionString,
            sql => sql.MigrationsAssembly(migrationsAssembly));
    })
    .AddOperationalStore(options =>
    {
        options.ConfigureDbContext = b => b.UseSqlServer(connectionString,
            sql => sql.MigrationsAssembly(migrationsAssembly));
    }).AddDeveloperSigningCredential();
builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    //Disable account confirmation.
    options.SignIn.RequireConfirmedAccount = false;
    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedPhoneNumber = false;
})
            .AddEntityFrameworkStores<ApplicationDbContext>();
var app = builder.Build();
app.UseIdentityServer();

app.MapGet("/", () => "Hello World!");

app.Run();
