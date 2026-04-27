
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;   
using System.Text;  
using CodeLingoAPI.Data;

var builder = WebApplication.CreateBuilder(args);


// 2. JWT Authentication
var jwtKey = "CodeLingoSuperSecretKey2024!XYZ";
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtKey)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

// 3. Controllers
builder.Services.AddControllers();

// ── Build the app 
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
}

app.UseAuthorization();

app.MapControllers();
app.Run();

app.Run();
