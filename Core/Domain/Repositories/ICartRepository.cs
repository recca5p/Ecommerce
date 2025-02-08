using Domain.Entities;

namespace Domain.RepositoriyInterfaces;

public interface ICartRepository
{
    Task<Cart?> GetByUserIdAsync(long userId, CancellationToken cancellationToken = default);
    void Insert(Cart cart);
    void Update(Cart? cart);
}
