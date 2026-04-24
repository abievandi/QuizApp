
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;   
using System.Text;  
using CodeLingoAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// 1. SQLite database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=codelingo.db"));

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

// 4. Swagger — needs Swashbuckle.AspNetCore NuGet package installed
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ── Build the app 
var app = builder.Build();

// 5. Create database tables + seed on first run
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
    DbSeeder.Seed(db);
}

// 6. Swagger UI — only show in Development mode
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 7. Auth + routing
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

