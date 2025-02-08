using Domain.Entities;

namespace Domain.Repositories;

public interface IUserRepository
{
    Task<IEnumerable<User?>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<User?> GetByIdAsync(long ID, CancellationToken cancellationToken = default);
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    void Insert(User? user);
    void Update(User? user);
    void Remove(User? user);
}
