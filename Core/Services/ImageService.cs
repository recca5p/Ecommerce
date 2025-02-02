using AutoMapper;
using Contract.DTOs.Request;
using Contract.DTOs.Response;
using Domain.Entities;
using Domain.RepositoriyInterfaces;
using Services.Abstraction;

namespace Services;

internal sealed class ImageService : IImageService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IMapper _mapper;

    public ImageService(IRepositoryManager repositoryManager, IMapper mapper)
    {
        _repositoryManager = repositoryManager;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ImageDto>> GetAllByProductIdAsync(long productId, CancellationToken cancellationToken = default)
    {
        var images = await _repositoryManager.ImageRepository.GetAllByProductIdAsync(productId, cancellationToken);
        return _mapper.Map<IEnumerable<ImageDto>>(images);
    }

    public async Task CreateAsync(ImageForCreationDto imageDto, long productId, CancellationToken cancellationToken = default)
    {
        var image = _mapper.Map<Image>(imageDto);
        image.ProductId = productId;

        _repositoryManager.ImageRepository.Insert(image);
        await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(long id, CancellationToken cancellationToken = default)
    {
        var image = await _repositoryManager.ImageRepository.GetByIdAsync(id, cancellationToken);
        if (image == null)
        {
            throw new Exception($"Image with ID {id} not found.");
        }

        _repositoryManager.ImageRepository.Remove(image);
        await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
    }
}
