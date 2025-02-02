using Contract.DTOs.Request;
using Contract.DTOs.Response;

namespace Services.Abstraction;

public interface IOrderService
{
    Task<IEnumerable<OrderDto>> GetAllByUserIdAsync(long userId, CancellationToken cancellationToken = default);
    Task<OrderDetailDto> GetOrderDetailsAsync(long id, CancellationToken cancellationToken = default);
    Task<OrderDto> CreateAsync(OrderForCreationDto orderDto, CancellationToken cancellationToken = default);
    Task UpdateAsync(long id, OrderForUpdateDto orderDto, CancellationToken cancellationToken = default);
    Task DeleteAsync(long id, CancellationToken cancellationToken = default);
}
