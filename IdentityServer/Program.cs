using IdentityServer;
using IdentityServer.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
//var seed = args.Contains("/seed");
//if (seed)
//{
//    args = args.Except(new[] { "/seed" }).ToArray();
//}
var builder = WebApplication.CreateBuilder(args);
var migrationsAssembly = typeof(Program).GetTypeInfo().Assembly.GetName().Name;
const string connectionString = @"Data Source=RSPLLT754\MSSQLSERVER01;Initial Catalog=Product_MicroServices;Integrated Security=True;TrustServerCertificate=True";
//if (seed)
//{
//    SeedData.EnsureSeedData(connectionString);
//}
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AspNetIdentityContext>(o => o.UseSqlServer(connectionString, b => b.MigrationsAssembly(migrationsAssembly)));
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AspNetIdentityContext>();
builder.Services.AddScoped<IDbinitalizer, DbInitializer>();
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
//builder.Services.AddIdentityServer()
//        .AddInMemoryIdentityResources(Configg.IdentityResources)
//        .AddInMemoryApiScopes(Configg.ApiScopes)
//        .AddTestUsers(Configg.testUsers)
//        .AddInMemoryClients(Configg.Clients)
//        .AddDeveloperSigningCredential();
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
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
SeedDatabase();
app.UseIdentityServer();


app.UseAuthorization();
app.UseCors("sam");
app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});
app.Run();
void SeedDatabase()
{
    using (var scope = app.Services.CreateScope())
    {
        var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbinitalizer>();
        dbInitializer.Initialize();
    }
}
