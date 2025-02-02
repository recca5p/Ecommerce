using Domain.Entities;

namespace Domain.Repositories;

public interface ICartItemRepository
{
    Task<CartItem?> GetByIdAsync(long id, CancellationToken cancellationToken = default);
    void Insert(CartItem cartItem);
    void Update(CartItem cartItem);
    void Remove(CartItem cartItem);
}
