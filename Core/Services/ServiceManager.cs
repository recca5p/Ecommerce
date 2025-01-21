using AutoMapper;
using Domain.RepositoriyInterfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Services.Abstraction;

namespace Services;

public sealed class ServiceManager
{
    private readonly Lazy<IProductService> _lazyProductService;
    
    public ServiceManager(IRepositoryManager repositoryManager,
        IMapper mapper)
    {
        _lazyProductService = new Lazy<IProductService>(() => new ProductService(repositoryManager, mapper));
    }
    
    public IProductService ProductService => _lazyProductService.Value;
}