using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class NewsContext : DbContext
{
    public DbSet<Heading> Headings { get; set; }
    public DbSet<News> News { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<User> Users { get; set; }

    public NewsContext() { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=NewsDb;Trusted_Connection=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<User>()
            .HasMany<News>(u => u.News);

        modelBuilder.Entity<Heading>().HasData(
            new Heading { Id = 1, Title = "sport" },
            new Heading { Id = 2, Title = "politic" },
            new Heading { Id = 3, Title = "warfare" },
            new Heading { Id = 4, Title = "nature" },
            new Heading { Id = 5, Title = "art" });
    }
}