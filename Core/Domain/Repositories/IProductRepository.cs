using Domain.Entities;

namespace Domain.Repositories;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Product> GetByIdAsync(long ID, CancellationToken cancellationToken = default);
    void Insert(Product owner);
    void Update(Product owner);
    void Remove(Product owner);
}