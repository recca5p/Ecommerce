using Domain.Entities;

namespace Domain.Repositories;

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Category> GetByIdAsync(long ID, CancellationToken cancellationToken = default);
    void Insert(Category owner);
    void Update(Category owner);
    void Remove(Category owner);
}