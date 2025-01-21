using Domain.Repositories;
using Domain.RepositoriyInterfaces;

namespace Persistence.Repositories;

public sealed class RepositoryManager : IRepositoryManager
{
    private readonly Lazy<IUnitOfWork> _lazyUnitOfWork;
    private readonly Lazy<IProductRepository> _lazyProductRepository;

    public RepositoryManager(IRepositoryDbContext repositoryManager)
    {
        _lazyProductRepository = new Lazy<IProductRepository>(() => new ProductRepository(repositoryManager));
    }
    public IUnitOfWork UnitOfWork => _lazyUnitOfWork.Value;
    
    public IProductRepository ProductRepository => _lazyProductRepository.Value;
}