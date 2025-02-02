using AutoMapper;
using Contract.DTOs.Request;
using Contract.DTOs.Response;
using Domain.Entities;
using Domain.RepositoriyInterfaces;
using Services.Abstraction;

namespace Services;

internal sealed class ReviewService : IReviewService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IMapper _mapper;

    public ReviewService(IRepositoryManager repositoryManager, IMapper mapper)
    {
        _repositoryManager = repositoryManager;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ReviewDto>> GetAllByProductIdAsync(long productId, CancellationToken cancellationToken = default)
    {
        var reviews = await _repositoryManager.ReviewRepository.GetAllByProductIdAsync(productId, cancellationToken);
        return _mapper.Map<IEnumerable<ReviewDto>>(reviews);
    }

    public async Task CreateAsync(ReviewForCreationDto reviewDto, CancellationToken cancellationToken = default)
    {
        var review = _mapper.Map<Review>(reviewDto);
        _repositoryManager.ReviewRepository.Insert(review);
        await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
    }
}
