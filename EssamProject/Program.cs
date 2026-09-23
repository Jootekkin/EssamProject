using EssamProject.Minimal_API;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
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

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "your-issuer",
        ValidAudience = "your-audience",
        IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("ThisIsMySecretKeyForJwtAuthentication12345"))
    };
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

app.MapGet("/Login", ([FromQuery] string userName, string password, HttpContext context) =>
{
    var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, userName),
        new Claim(ClaimTypes.Role, "Admin"),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
    };

    var key = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("ThisIsMySecretKeyForJwtAuthentication12345"));

    var securityToken = new JwtSecurityToken(
        issuer: "your-issuer",
        audience: "your-audience",
        claims: claims,
        expires: DateTime.Now.AddHours(1),
        signingCredentials: new Microsoft.IdentityModel.Tokens.SigningCredentials(key, SecurityAlgorithms.HmacSha256)
    );

    var tokenHandler = new JwtSecurityTokenHandler().WriteToken(securityToken);

    context.Response.Headers.Add("Authorization", $"Bearer {tokenHandler}");

    return $"Login successful for user: {userName}";
});

app.MapGet("/Logout", (HttpContext context) =>
{
    context.Response.Headers.Remove("Authorization");
});

app.MapGet("/anonymous", [Authorize(Roles = "User")] () => "anonymous");

app.MapGet("/secret", [Authorize(Roles = "Admin")] () => "secret");
#endregion

app.UseHsts();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
