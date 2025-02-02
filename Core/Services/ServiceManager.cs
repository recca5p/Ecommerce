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

    public ServiceManager(IRepositoryManager repositoryManager, IMapper mapper)
    {
        _lazyProductService = new Lazy<IProductService>(() => new ProductService(repositoryManager, mapper));
        _lazyProductVariantService = new Lazy<IProductVariantService>(() => new ProductVariantService(repositoryManager, mapper));
        _lazyCategoryService = new Lazy<ICategoryService>(() => new CategoryService(repositoryManager, mapper));
        _lazyUserService = new Lazy<IUserService>(() => new UserService(repositoryManager, mapper));
        _lazyImageService = new Lazy<IImageService>(() => new ImageService(repositoryManager, mapper));
    }

    public IProductService ProductService => _lazyProductService.Value;
    public IProductVariantService ProductVariantService => _lazyProductVariantService.Value;
    public ICategoryService CategoryService => _lazyCategoryService.Value;
    public IUserService UserService => _lazyUserService.Value;
    public IImageService ImageService => _lazyImageService.Value;
}
