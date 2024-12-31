using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

internal sealed class ProductRepository : IProductRepository
{
    private readonly RepositoryDbContext _dbContext;
    
    public ProductRepository(RepositoryDbContext dbContext) => _dbContext = dbContext;
    
    public async Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken = default) =>
        await _dbContext.Products.ToListAsync(cancellationToken);
    
    public async Task<Product> GetByIdAsync(long ID, CancellationToken cancellationToken = default) =>
        await _dbContext.Products.FirstOrDefaultAsync(x => x.ProductId == ID, cancellationToken);
    
    public void Insert(Product product) => _dbContext.Products.Add(product);
    
    public void Update(Product product) => _dbContext.Products.Add(product);
    
    public void Remove(Product product) => _dbContext.Products.Remove(product);
}