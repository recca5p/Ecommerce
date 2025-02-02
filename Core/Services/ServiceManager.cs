using AutoMapper;
using Domain.RepositoriyInterfaces;
using Services.Abstraction;

namespace Services;

public sealed class ServiceManager : IServiceManager
{
    private readonly Lazy<IProductService> _lazyProductService;
    private readonly Lazy<IProductVariantService> _lazyProductVariantService;
    private readonly Lazy<ICategoryService> _lazyCategoryService;
    private readonly Lazy<IUserService> _lazyUserService;
    private readonly Lazy<IImageService> _lazyImageService;
    private readonly Lazy<IOrderService> _lazyOrderService;
    private readonly Lazy<ICartService> _lazyCartService;
    private readonly Lazy<IPaymentService> _lazyPaymentService; // Payment Service
    private readonly Lazy<IReviewService> _lazyReviewService;   // Review Service

    public ServiceManager(IRepositoryManager repositoryManager, IMapper mapper)
    {
        _lazyProductService = new Lazy<IProductService>(() => new ProductService(repositoryManager, mapper));
        _lazyProductVariantService = new Lazy<IProductVariantService>(() => new ProductVariantService(repositoryManager, mapper));
        _lazyCategoryService = new Lazy<ICategoryService>(() => new CategoryService(repositoryManager, mapper));
        _lazyUserService = new Lazy<IUserService>(() => new UserService(repositoryManager, mapper));
        _lazyImageService = new Lazy<IImageService>(() => new ImageService(repositoryManager, mapper));
        _lazyOrderService = new Lazy<IOrderService>(() => new OrderService(repositoryManager, mapper));
        _lazyCartService = new Lazy<ICartService>(() => new CartService(repositoryManager, mapper));
        _lazyPaymentService = new Lazy<IPaymentService>(() => new PaymentService(repositoryManager, mapper)); // Add Payment
        _lazyReviewService = new Lazy<IReviewService>(() => new ReviewService(repositoryManager, mapper));    // Add Review
    }

    public IProductService ProductService => _lazyProductService.Value;
    public IProductVariantService ProductVariantService => _lazyProductVariantService.Value;
    public ICategoryService CategoryService => _lazyCategoryService.Value;
    public IUserService UserService => _lazyUserService.Value;
    public IImageService ImageService => _lazyImageService.Value;
    public IOrderService OrderService => _lazyOrderService.Value;
    public ICartService CartService => _lazyCartService.Value;
    public IPaymentService PaymentService => _lazyPaymentService.Value; // Expose Payment
    public IReviewService ReviewService => _lazyReviewService.Value;    // Expose Review
}
