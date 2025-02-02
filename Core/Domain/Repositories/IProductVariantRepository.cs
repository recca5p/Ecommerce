using Domain.Entities;

namespace Domain.Repositories;

public interface IProductVariantRepository
{
    Task<IEnumerable<ProductVariant>> GetAllByProductIdAsync(long productId, CancellationToken cancellationToken = default);
    Task<ProductVariant?> GetByIdAsync(long variantId, CancellationToken cancellationToken = default);
    void Insert(ProductVariant variant);
    void Remove(ProductVariant variant);
}
