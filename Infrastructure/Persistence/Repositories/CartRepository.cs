using Domain.Entities;
using Domain.RepositoriyInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

internal sealed class CartRepository : ICartRepository
{
    private readonly IRepositoryDbContext _dbContext;

    public CartRepository(IRepositoryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Cart?> GetByUserIdAsync(long userId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Carts.Include(c => c.CartItems)
            .FirstOrDefaultAsync(c => c.UserId == userId, cancellationToken);
    }

    public void Insert(Cart cart)
    {
        _dbContext.Carts.Add(cart);
    }
}
