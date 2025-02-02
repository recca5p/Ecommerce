using Domain.Entities;
using Domain.Repositories;
using Domain.RepositoriyInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

internal sealed class CartItemRepository : ICartItemRepository
{
    private readonly IRepositoryDbContext _dbContext;

    public CartItemRepository(IRepositoryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<CartItem?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.CartItems
            .Include(ci => ci.ProductVariant) // Sử dụng ProductVariant thay vì Product
            .ThenInclude(pv => pv.Product)   // Nếu cần thông tin Product, truy cập thông qua ProductVariant
            .FirstOrDefaultAsync(ci => ci.CartItemId == id, cancellationToken);
    }

    public void Insert(CartItem cartItem) => _dbContext.CartItems.Add(cartItem);

    public void Update(CartItem cartItem) => _dbContext.CartItems.Update(cartItem);

    public void Remove(CartItem cartItem) => _dbContext.CartItems.Remove(cartItem);
}
