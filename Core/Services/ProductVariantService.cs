using AutoMapper;
using Contract.DTOs.Request;
using Contract.DTOs.Response;
using Domain.Entities;
using Domain.Exceptions;
using Domain.RepositoriyInterfaces;
using Services.Abstraction;

namespace Services;

internal sealed class ProductVariantService : IProductVariantService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IMapper _mapper;

    public ProductVariantService(IRepositoryManager repositoryManager, IMapper mapper)
    {
        _repositoryManager = repositoryManager;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductVariantDto>> GetAllByProductIdAsync(long productId, CancellationToken cancellationToken = default)
    {
        var variants = await _repositoryManager.ProductVariantRepository.GetAllByProductIdAsync(productId, cancellationToken);
        return _mapper.Map<IEnumerable<ProductVariantDto>>(variants);
    }

    public async Task<ProductVariantDto> GetByIdAsync(long variantId, CancellationToken cancellationToken = default)
    {
        var variant = await _repositoryManager.ProductVariantRepository.GetByIdAsync(variantId, cancellationToken);
        if (variant == null)
        {
            throw new Exception($"Variant with ID {variantId} not found.");
        }
        return _mapper.Map<ProductVariantDto>(variant);
    }

    public async Task CreateAsync(ProductVariantForCreationDto variantDto, long productId, CancellationToken cancellationToken = default)
    {
        var variant = _mapper.Map<ProductVariant>(variantDto);
        variant.ProductId = productId;

        variant.CreatedDate = DateTime.UtcNow;
        variant.CreatedById = 123;


        _repositoryManager.ProductVariantRepository.Insert(variant);
        await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(long variantId, ProductVariantForUpdateDto variantDto, CancellationToken cancellationToken = default)
    {
        var variant = await _repositoryManager.ProductVariantRepository.GetByIdAsync(variantId, cancellationToken);
        if (variant == null)
        {
            throw new Exception($"Variant with ID {variantId} not found.");
        }

        variant.Color = variantDto.Color;
        variant.Storage = variantDto.Storage;
        variant.Price = variantDto.Price;
        variant.Stock = variantDto.Stock;

        await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(long variantId, CancellationToken cancellationToken = default)
    {
        var variant = await _repositoryManager.ProductVariantRepository.GetByIdAsync(variantId, cancellationToken);
        if (variant == null)
        {
            throw new Exception($"Variant with ID {variantId} not found.");
        }

        _repositoryManager.ProductVariantRepository.Remove(variant);
        await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
    }
}
