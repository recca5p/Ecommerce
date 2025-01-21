using Domain.Entities;
using Domain.Repositories;
using Domain.RepositoriyInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

internal sealed class ProductRepository : IProductRepository
{
    private readonly IRepositoryDbContext _dbContext;
    
    public ProductRepository(IRepositoryDbContext dbContext) => _dbContext = dbContext;
    
    public async Task<IEnumerable<Product?>> GetAllAsync(CancellationToken cancellationToken = default) =>
        await _dbContext.Products.Where(_ => _.IsDeleted == false).ToListAsync( cancellationToken);
    
    public async Task<Product?> GetByIdAsync(long ID, CancellationToken cancellationToken = default) =>
        await _dbContext.Products.FirstOrDefaultAsync(x => x.ProductId == ID && x.IsDeleted == false, cancellationToken);
    
    public void Insert(Product? product) => _dbContext.Products.Add(product);
    
    public void Update(Product? product) => _dbContext.Products.Update(product);

    public void Remove(Product? product)
    {
        if (product != null)
        {
            product.IsDeleted = true;
            _dbContext.Products.Update(product);
        }
    }
}