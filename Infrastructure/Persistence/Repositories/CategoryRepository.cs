using Domain.Entities;
using Domain.Repositories;
using Domain.RepositoriyInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

internal sealed class CategoryRepository : ICategoryRepository
{
    private readonly IRepositoryDbContext _dbContext;

    public CategoryRepository(IRepositoryDbContext dbContext) => _dbContext = dbContext;

    public async Task<IEnumerable<Category?>> GetAllAsync(CancellationToken cancellationToken = default) =>
        await _dbContext.Categories.Where(c => !c.IsDeleted).ToListAsync(cancellationToken);

    public async Task<Category?> GetByIdAsync(long ID, CancellationToken cancellationToken = default) =>
        await _dbContext.Categories.FirstOrDefaultAsync(c => c.CategoryId == ID && !c.IsDeleted, cancellationToken);

    public void Insert(Category? category) => _dbContext.Categories.Add(category);

    public void Update(Category? category) => _dbContext.Categories.Update(category);

    public void Remove(Category? category)
    {
        if (category != null)
        {
            category.IsDeleted = true;
            _dbContext.Categories.Update(category);
        }
    }
}
