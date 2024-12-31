using System.Reflection;
using Domain.Entities;
using Domain.RepositoriyInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public sealed class RepositoryDbContext : DbContext, IRepositoryDbContext
{
    public RepositoryDbContext()
    {
    }

    public RepositoryDbContext(DbContextOptions<RepositoryDbContext> options)
        : base(options)
    {
    }
        
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // var connectionString = Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING");
        // if (!string.IsNullOrEmpty(connectionString))
        // {
        //     optionsBuilder.UseNpgsql(connectionString);
        // }
        optionsBuilder.UseNpgsql("DATABASE_CONNECTION_STRING");
    }
    
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RepositoryDbContext).Assembly);
}