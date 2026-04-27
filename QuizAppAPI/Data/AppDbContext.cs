using Microsoft.EntityFrameworkCore;
using Quiz.Models;

public class AppDbContext : DbContext
{
    public DbSet<Result> Results { get; set; }
}