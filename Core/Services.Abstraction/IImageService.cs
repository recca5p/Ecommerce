using Contract.DTOs.Request;
using Contract.DTOs.Response;

namespace Services.Abstraction;

public interface IImageService
{
    Task<IEnumerable<ImageDto>> GetAllByProductIdAsync(long productId, CancellationToken cancellationToken = default);
    Task CreateAsync(ImageForCreationDto imageDto, long productId, CancellationToken cancellationToken = default);
    Task DeleteAsync(long id, CancellationToken cancellationToken = default);
}
