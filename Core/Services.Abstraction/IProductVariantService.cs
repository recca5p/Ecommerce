using Contract.DTOs.Request;
using Contract.DTOs.Response;

namespace Services.Abstraction;

public interface IProductVariantService
{
    Task<IEnumerable<ProductVariantDto>> GetAllByProductIdAsync(long productId, CancellationToken cancellationToken = default);
    Task<ProductVariantDto> GetByIdAsync(long variantId, CancellationToken cancellationToken = default);
    Task CreateAsync(ProductVariantForCreationDto variantDto, long productId, CancellationToken cancellationToken = default);
    Task UpdateAsync(long variantId, ProductVariantForUpdateDto variantDto, CancellationToken cancellationToken = default);
    Task DeleteAsync(long variantId, CancellationToken cancellationToken = default);
}
