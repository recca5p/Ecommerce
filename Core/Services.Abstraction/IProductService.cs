using Contract.DTOs.Request;
using Contract.DTOs.Response;
using Domain.Entities;

namespace Services.Abstraction;

public interface IProductService
{
    Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<ProductDto> GetByIdAsync(long ID, CancellationToken cancellationToken = default);
    Task<ProductDto> CreateAsync(ProductForCreationDto productForCreationDto, CancellationToken cancellationToken = default);
    Task UpdateAsync(long ID, ProductForUpdateDto productForUpdateDto, CancellationToken cancellationToken = default);
    Task DeleteAsync(long ID, CancellationToken cancellationToken = default);
}