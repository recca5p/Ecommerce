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
    private readonly Lazy<IOrderRepository> _lazyOrderRepository;
    private readonly Lazy<ICartRepository> _lazyCartRepository;
    private readonly Lazy<ICartItemRepository> _lazyCartItemRepository;
    private readonly Lazy<IPaymentRepository> _lazyPaymentRepository; // Thêm dòng này
    private readonly Lazy<IReviewRepository> _lazyReviewRepository;   // Thêm dòng này

    public RepositoryManager(IRepositoryDbContext repositoryDbContext)
    {
        _lazyProductRepository = new Lazy<IProductRepository>(() => new ProductRepository(repositoryDbContext));
        _lazyUserRepository = new Lazy<IUserRepository>(() => new UserRepository(repositoryDbContext));
        _lazyCategoryRepository = new Lazy<ICategoryRepository>(() => new CategoryRepository(repositoryDbContext));
        _lazyProductVariantRepository = new Lazy<IProductVariantRepository>(() => new ProductVariantRepository(repositoryDbContext));
        _lazyImageRepository = new Lazy<IImageRepository>(() => new ImageRepository(repositoryDbContext));
        _lazyOrderRepository = new Lazy<IOrderRepository>(() => new OrderRepository(repositoryDbContext));
        _lazyCartRepository = new Lazy<ICartRepository>(() => new CartRepository(repositoryDbContext));
        _lazyCartItemRepository = new Lazy<ICartItemRepository>(() => new CartItemRepository(repositoryDbContext));
        _lazyPaymentRepository = new Lazy<IPaymentRepository>(() => new PaymentRepository(repositoryDbContext)); // Thêm dòng này
        _lazyReviewRepository = new Lazy<IReviewRepository>(() => new ReviewRepository(repositoryDbContext));    // Thêm dòng này
    }

    public IUnitOfWork UnitOfWork => _lazyUnitOfWork.Value;

    public IProductRepository ProductRepository => _lazyProductRepository.Value;
    public IUserRepository UserRepository => _lazyUserRepository.Value;
    public ICategoryRepository CategoryRepository => _lazyCategoryRepository.Value;
    public IProductVariantRepository ProductVariantRepository => _lazyProductVariantRepository.Value;
    public IImageRepository ImageRepository => _lazyImageRepository.Value;
    public IOrderRepository OrderRepository => _lazyOrderRepository.Value;
    public ICartRepository CartRepository => _lazyCartRepository.Value;
    public ICartItemRepository CartItemRepository => _lazyCartItemRepository.Value;
    public IPaymentRepository PaymentRepository => _lazyPaymentRepository.Value; // Expose PaymentRepository
    public IReviewRepository ReviewRepository => _lazyReviewRepository.Value;    // Expose ReviewRepository
}
