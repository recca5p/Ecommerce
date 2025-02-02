using Contract.DTOs.Request;
using Contract.DTOs.Response;

namespace Services.Abstraction;

public interface IReviewService
{
    Task<IEnumerable<ReviewDto>> GetAllByProductIdAsync(long productId, CancellationToken cancellationToken = default);
    Task CreateAsync(ReviewForCreationDto reviewDto, CancellationToken cancellationToken = default);
}
