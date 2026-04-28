using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using CodeLingoAPI.Data;
using CodeLingoAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// 🔹 DATABASE (SQLite example)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=quizapp.db"));

// 🔹 AI SERVICE
builder.Services.AddScoped<AIQuizService>();

// 🔹 JWT AUTH
var jwtKey = "CodeLingoSuperSecretKey2024!XYZ";

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey =
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

// 🔹 CONTROLLERS
builder.Services.AddControllers();

var app = builder.Build();

// 🔹 MIDDLEWARE
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();