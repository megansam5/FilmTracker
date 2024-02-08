using FilmTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace FilmTracker.Data;

public class DataContext : DbContext
{
    public DbSet<User> UserTable { get; set; } = null!;
    public DbSet<Movie> Movies { get; set; } = null!;
    
    public DataContext() { }

    public DataContext(DbContextOptions<DataContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasKey(u => u.UserId);
        modelBuilder.Entity<Movie>().HasKey(m => new { m.UserId, m.imdbID });
    }

}