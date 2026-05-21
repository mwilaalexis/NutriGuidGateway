using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using UserProfileServiceProject.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

//  AUTHENTICATION JWT POUR OCELOT 
builder.ConfigureAuthentication();
builder.ConfigureAuthorizationPolicy();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});
// Ajouter Ocelot
builder.Services.AddOcelot(builder.Configuration);

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseCors("AllowAll");

app.Use(async (context, next) =>
{
    Console.WriteLine($"➡ Gateway received → {context.Request.Method} {context.Request.Path}");
    Console.WriteLine($"   Authorization Header: {context.Request.Headers["Authorization"]}");
    await next.Invoke();
});

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

await app.UseOcelot();

app.Run();
