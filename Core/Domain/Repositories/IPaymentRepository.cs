using Domain.Entities;

namespace Domain.RepositoriyInterfaces;

public interface IPaymentRepository
{
    Task<IEnumerable<Payment>> GetAllByUserIdAsync(long userId, CancellationToken cancellationToken = default);
    Task<Payment?> GetByIdAsync(long paymentId, CancellationToken cancellationToken = default);
    void Insert(Payment payment);
    void Remove(Payment payment);
}
