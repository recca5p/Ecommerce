using Domain.Repositories;
using Domain.RepositoriyInterfaces;

namespace Persistence.Repositories;

public sealed class RepositoryManager : IRepositoryManager
{
    private readonly Lazy<IUnitOfWork> _lazyUnitOfWork;
    private readonly Lazy<IProductRepository> _lazyProductRepository;
    private readonly Lazy<IUserRepository> _lazyUserRepository;
    private readonly Lazy<ICategoryRepository> _lazyCategoryRepository;

    public RepositoryManager(IRepositoryDbContext repositoryManager)
    {
        _lazyProductRepository = new Lazy<IProductRepository>(() => new ProductRepository(repositoryManager));
        _lazyUserRepository = new Lazy<IUserRepository>(() => new UserRepository(repositoryManager));
        _lazyCategoryRepository = new Lazy<ICategoryRepository>(() => new CategoryRepository(repositoryManager));
    }
    public IUnitOfWork UnitOfWork => _lazyUnitOfWork.Value;
    
    public IProductRepository ProductRepository => _lazyProductRepository.Value;
    public IUserRepository UserRepository => _lazyUserRepository.Value;
    public ICategoryRepository CategoryRepository => _lazyCategoryRepository.Value;
}