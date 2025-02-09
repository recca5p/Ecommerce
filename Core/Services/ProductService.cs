using AutoMapper;
using Contract.DTOs.Request;
using Contract.DTOs.Response;
using Domain.Entities;
using Domain.Exceptions;
using Domain.RepositoriyInterfaces;
using Services.Abstraction;

namespace Services;

internal sealed class ProductService : IProductService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IMapper _mapper;

    public ProductService(IRepositoryManager repositoryManager, IMapper mapper)
    {
        _repositoryManager = repositoryManager;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<ProductDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var products = await _repositoryManager.ProductRepository.GetAllAsync(cancellationToken);
        
        return _mapper.Map<IEnumerable<ProductDto>>(products);;
    }
    public async Task<ProductDto> GetByIdAsync(long ID, CancellationToken cancellationToken = default)
    {
        var product = await _repositoryManager.ProductRepository.GetByIdAsync(ID, cancellationToken);
        if (product is null)
        {
            throw new ProductNotFoundException(ID);
        }
        
        return _mapper.Map<ProductDto>(product);
    }
    public async Task CreateAsync(ProductForCreationDto productRequest, CancellationToken cancellationToken = default)
    {
        var product = _mapper.Map<Product>(productRequest);

        product.CreatedDate = DateTime.UtcNow;
        product.CreatedById = 123;

        _repositoryManager.ProductRepository.Insert(product);
        
        await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
    }
    public async Task UpdateAsync(long ID, ProductForUpdateDto productRequest, CancellationToken cancellationToken = default)
    {
        var product = await _repositoryManager.ProductRepository.GetByIdAsync(ID, cancellationToken);
        if (product is null)
        {
            throw new ProductNotFoundException(ID);
        }
        product.ProductName = productRequest.ProductName;
        product.Description = productRequest.Description;
        product.CategoryId = productRequest.CategoryId;
        product.Category = await _repositoryManager.CategoryRepository.GetByIdAsync(productRequest.CategoryId, cancellationToken);
        
        _repositoryManager.ProductRepository.Update(product);
        
        await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
    }
    public async Task DeleteAsync(long ID, CancellationToken cancellationToken = default)
    {
        var product = await _repositoryManager.ProductRepository.GetByIdAsync(ID, cancellationToken);
        if (product is null)
        {
            throw new ProductNotFoundException(ID);
        }
        _repositoryManager.ProductRepository.Remove(product);
        await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
    }
}