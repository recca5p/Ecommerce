using Domain.Entities;
using Domain.RepositoriyInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

internal sealed class ReviewRepository : IReviewRepository
{
    private readonly IRepositoryDbContext _dbContext;

    public ReviewRepository(IRepositoryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Review>> GetAllByProductIdAsync(long productId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Reviews
            .Where(r => r.ProductId == productId && !r.IsDeleted)
            .ToListAsync(cancellationToken);
    }

    public void Insert(Review? review) => _dbContext.Reviews.Add(review);
}
