using EssamProject.Minimal_API;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Versioning;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    options.ReportApiVersions = true;
    //options.ApiVersionReader = new UrlSegmentApiVersionReader();
    //options.ApiVersionReader = new QueryStringApiVersionReader("api-version");
    //options.ApiVersionReader = new HeaderApiVersionReader("X-Version");
    options.ApiVersionReader = new MediaTypeApiVersionReader("ver");
});

builder.Services.AddAuthentication().AddCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
});

builder.Services.AddAuthorization();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}


#region Tests
app.MapGet("/Welcome", () => "Welcome to first minimal API");

app.MapGroupingMinimal();

app.MapGet("/Login", (HttpContext context) =>
{

    var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, "Essam"),
        new Claim(ClaimTypes.Role, "Admin")
    };

    var claimsIdentity = new ClaimsIdentity(claims, "Cookies");

    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

    context.SignInAsync("Cookies", claimsPrincipal);

    context.Response.WriteAsync("Login successful");
});

app.MapGet("/Logout", (HttpContext context) =>
{
    context.SignOutAsync("Cookies");
});

app.MapGet("/anonymous", () => "anonymous");

app.MapGet("/secret", [Authorize] () => "secret");
#endregion

app.UseHsts();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
