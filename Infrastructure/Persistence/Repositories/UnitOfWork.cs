using Domain.Repositories;
using Domain.RepositoriyInterfaces;

namespace Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly IRepositoryDbContext _context;

    public UnitOfWork(IRepositoryDbContext context)
    {
        _context = context;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}