using Domain.Entities;

namespace Domain.RepositoriyInterfaces;

public interface IOrderRepository
{
    Task<IEnumerable<Order>> GetAllByUserIdAsync(long userId, CancellationToken cancellationToken = default);
    Task<Order?> GetByIdWithDetailsAsync(long id, CancellationToken cancellationToken = default);
    Task<Order?> GetByIdAsync(long id, CancellationToken cancellationToken = default);
    void Insert(Order order);
    void Remove(Order order);
}
