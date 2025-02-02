using Contract.DTOs.Request;
using Contract.DTOs.Response;

namespace Services.Abstraction;

public interface IPaymentService
{
    Task<IEnumerable<PaymentDto>> GetAllByUserIdAsync(long userId, CancellationToken cancellationToken = default);
    Task<PaymentDto> GetByIdAsync(long paymentId, CancellationToken cancellationToken = default);
    Task CreateAsync(PaymentForCreationDto paymentDto, CancellationToken cancellationToken = default);
    Task UpdateAsync(long paymentId, PaymentForUpdateDto paymentDto, CancellationToken cancellationToken = default);
    Task DeleteAsync(long paymentId, CancellationToken cancellationToken = default);
}
