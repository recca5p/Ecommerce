using Domain.Entities;
using Domain.Repositories;
using Domain.RepositoriyInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

internal sealed class ProductVariantRepository : IProductVariantRepository
{
    private readonly IRepositoryDbContext _dbContext;

    public ProductVariantRepository(IRepositoryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<ProductVariant>> GetAllByProductIdAsync(long productId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.ProductVariants
            .Where(v => v.ProductId == productId)
            .ToListAsync(cancellationToken);
    }

    public async Task<ProductVariant?> GetByIdAsync(long variantId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.ProductVariants
            .FirstOrDefaultAsync(v => v.VariantId == variantId, cancellationToken);
    }

    public void Insert(ProductVariant variant)
    {
        _dbContext.ProductVariants.Add(variant);
    }
    
    public void Update(ProductVariant variant)
    {
        _dbContext.ProductVariants.Update(variant);
    }

    public void Remove(ProductVariant variant)
    {
        _dbContext.ProductVariants.Remove(variant);
    }
}
