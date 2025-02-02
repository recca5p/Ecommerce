using Contract.DTOs.Request;
using Contract.DTOs.Response;

namespace Services.Abstraction;

public interface ICartService
{
    Task<CartDto> GetByUserIdAsync(long userId, CancellationToken cancellationToken = default);
    Task AddItemAsync(long userId, CartItemForCreationDto cartItemDto, CancellationToken cancellationToken = default);
    Task UpdateItemAsync(long userId, long itemId, CartItemForUpdateDto cartItemDto, CancellationToken cancellationToken = default);
    Task RemoveItemAsync(long userId, long itemId, CancellationToken cancellationToken = default);
}
