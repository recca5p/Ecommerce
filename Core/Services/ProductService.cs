using Contract.DTOs.Response;
using Domain.RepositoriyInterfaces;
using Services.Abstraction;

namespace Services;

internal sealed class ProductService : IProductService
{
    private readonly IRepositoryManager _repositoryManager;
    
    public ProductService(IRepositoryManager repositoryManager) => _repositoryManager = repositoryManager;
    
    public async Task<IEnumerable<ProductDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var owners = await _repositoryManager.OwnerRepository.GetAllAsync(cancellationToken);
        var ownersDto = owners.Adapt<IEnumerable<OwnerDto>>();
        return ownersDto;
    }
    public async Task<ProductDto> GetByIdAsync(Guid ownerId, CancellationToken cancellationToken = default)
    {
        var owner = await _repositoryManager.OwnerRepository.GetByIdAsync(ownerId, cancellationToken);
        if (owner is null)
        {
            throw new OwnerNotFoundException(ownerId);
        }
        var ownerDto = owner.Adapt<ProductDto>();
        return ownerDto;
    }
    public async Task<ProductDto> CreateAsync(OwnerForCreationDto ownerForCreationDto, CancellationToken cancellationToken = default)
    {
        var owner = ownerForCreationDto.Adapt<Owner>();
        _repositoryManager.OwnerRepository.Insert(owner);
        await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
        return owner.Adapt<OwnerDto>();
    }
    public async Task UpdateAsync(Guid ownerId, OwnerForUpdateDto ownerForUpdateDto, CancellationToken cancellationToken = default)
    {
        var owner = await _repositoryManager.OwnerRepository.GetByIdAsync(ownerId, cancellationToken);
        if (owner is null)
        {
            throw new OwnerNotFoundException(ownerId);
        }
        owner.Name = ownerForUpdateDto.Name;
        owner.DateOfBirth = ownerForUpdateDto.DateOfBirth;
        owner.Address = ownerForUpdateDto.Address;
        await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
    }
    public async Task DeleteAsync(Guid ownerId, CancellationToken cancellationToken = default)
    {
        var owner = await _repositoryManager.OwnerRepository.GetByIdAsync(ownerId, cancellationToken);
        if (owner is null)
        {
            throw new OwnerNotFoundException(ownerId);
        }
        _repositoryManager.OwnerRepository.Remove(owner);
        await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
    }
}