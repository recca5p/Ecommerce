using Domain.Repositories;
using Domain.RepositoriyInterfaces;

namespace Persistence.Repositories;

public sealed class RepositoryManager : IRepositoryManager
{
    private readonly Lazy<IUnitOfWork> _lazyUnitOfWork;
    private readonly Lazy<IProductRepository> _lazyProductRepository;
    private readonly Lazy<IUserRepository> _lazyUserRepository;
    private readonly Lazy<ICategoryRepository> _lazyCategoryRepository;
    private readonly Lazy<IProductVariantRepository> _lazyProductVariantRepository;
    private readonly Lazy<IImageRepository> _lazyImageRepository;

    public RepositoryManager(IRepositoryDbContext repositoryDbContext)
    {
        _lazyProductRepository = new Lazy<IProductRepository>(() => new ProductRepository(repositoryDbContext));
        _lazyUserRepository = new Lazy<IUserRepository>(() => new UserRepository(repositoryDbContext));
        _lazyCategoryRepository = new Lazy<ICategoryRepository>(() => new CategoryRepository(repositoryDbContext));
        _lazyProductVariantRepository = new Lazy<IProductVariantRepository>(() => new ProductVariantRepository(repositoryDbContext));
        _lazyImageRepository = new Lazy<IImageRepository>(() => new ImageRepository(repositoryDbContext));
    }
    public IUnitOfWork UnitOfWork => _lazyUnitOfWork.Value;

    public IProductRepository ProductRepository => _lazyProductRepository.Value;
    public IUserRepository UserRepository => _lazyUserRepository.Value;
    public ICategoryRepository CategoryRepository => _lazyCategoryRepository.Value;
    public IProductVariantRepository ProductVariantRepository => _lazyProductVariantRepository.Value;
    public IImageRepository ImageRepository => _lazyImageRepository.Value;
}
