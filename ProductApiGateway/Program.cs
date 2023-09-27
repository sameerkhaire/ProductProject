using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(p =>
{
    p.AddPolicy("sam", o =>
    {
        o.AllowAnyHeader();
        o.WithOrigins("http://localhost:4200");
        o.AllowAnyMethod();
    });
});
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
builder.Services.AddOcelot(builder.Configuration);




var app = builder.Build();
app.UseCors("sam");
app.UseOcelot().GetAwaiter().GetResult();
app.MapGet("/", () => "Hello World!");

app.Run();
