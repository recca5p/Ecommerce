using System.Reflection;
using Domain.Base;
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
        var connectionString = Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING");
        if (!string.IsNullOrEmpty(connectionString))
        {
            optionsBuilder.UseNpgsql(connectionString);
        }
        //optionsBuilder.UseNpgsql("DATABASE_CONNECTION_STRING");
    }
    
    public DbSet<Product?> Products { get; set; }
    public DbSet<User?> Users { get; set; }
    public DbSet<Category?> Categories { get; set; }
    public DbSet<ProductVariant?> ProductVariants { get; set; }
    public DbSet<Image?> Images { get; set; }
    public DbSet<Order?> Orders { get; set; }
    public DbSet<Cart?> Carts { get; set; }
    public DbSet<CartItem?> CartItems { get; set; }
    public DbSet<Payment?> Payments { get; set; }
    public DbSet<Review?> Reviews { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RepositoryDbContext).Assembly);
}