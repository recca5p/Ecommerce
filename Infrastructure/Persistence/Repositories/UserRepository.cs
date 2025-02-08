using Domain.Entities;
using Domain.Repositories;
using Domain.RepositoriyInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

internal sealed class UserRepository : IUserRepository
{
    private readonly IRepositoryDbContext _dbContext;

    public UserRepository(IRepositoryDbContext dbContext) => _dbContext = dbContext;

    public async Task<IEnumerable<User?>> GetAllAsync(CancellationToken cancellationToken = default) =>
        await _dbContext.Users.ToListAsync(cancellationToken);

    public async Task<User?> GetByIdAsync(long ID, CancellationToken cancellationToken = default) =>
        await _dbContext.Users.FirstOrDefaultAsync(u => u.UserId == ID, cancellationToken);
    
    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default) =>
        await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);

    public void Insert(User? user) => _dbContext.Users.Add(user);

    public void Update(User? user) => _dbContext.Users.Update(user);

    public void Remove(User? user)
    {
        if (user != null)
        {
            user.IsDeleted = true;
            _dbContext.Users.Update(user);
        }
    }
}
