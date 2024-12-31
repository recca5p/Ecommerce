using Domain.Repositories;
using Domain.RepositoriyInterfaces;

namespace Persistence.Repositories;

public sealed class RepositoryManager : IRepositoryManager
{
    private readonly Lazy<IProductRepository> _lazyProductRepository;

    public RepositoryManager(IRepositoryManager repositoryManager)
    {
        _lazyProductRepository = new Lazy<IProductRepository>(() => new );
    }
}