using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Domain.RepositoriyInterfaces;

public partial interface IRepositoryDbContext
{
    #region Methods

    /// <summary>
    /// Creates a DbSet that can be used to query and save instances of entity
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    /// <returns>A set for the given entity type</returns>
    DbSet<TEntity> Set<TEntity>() where TEntity : class;

    /// <summary>
    /// Saves all changes made in this context to the database
    /// </summary>
    /// <returns>The number of state entries written to the database</returns>
    Int32 SaveChanges();

    Task<Int32> SaveChangesAsync(CancellationToken cancellationToken = default);
    #endregion
    
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
}