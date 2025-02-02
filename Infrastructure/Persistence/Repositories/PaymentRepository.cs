using Domain.Entities;
using Domain.Repositories;
using Domain.RepositoriyInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

internal sealed class PaymentRepository : IPaymentRepository
{
    private readonly IRepositoryDbContext _dbContext;

    public PaymentRepository(IRepositoryDbContext dbContext) => _dbContext = dbContext;

    public async Task<IEnumerable<Payment>> GetAllByUserIdAsync(long userId, CancellationToken cancellationToken = default) =>
        await _dbContext.Payments.Where(p => p.Order.UserId == userId && !p.IsDeleted).ToListAsync(cancellationToken);

    public async Task<Payment?> GetByIdAsync(long paymentId, CancellationToken cancellationToken = default) =>
        await _dbContext.Payments.FirstOrDefaultAsync(p => p.PaymentId == paymentId && !p.IsDeleted, cancellationToken);

    public void Insert(Payment payment) => _dbContext.Payments.Add(payment);

    public void Update(Payment payment) => _dbContext.Payments.Update(payment);

    public void Remove(Payment payment)
    {
        payment.IsDeleted = true;
        _dbContext.Payments.Update(payment);
    }
}
