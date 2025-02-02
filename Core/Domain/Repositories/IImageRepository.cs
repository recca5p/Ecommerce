using Domain.Entities;

namespace Domain.RepositoriyInterfaces;

public interface IImageRepository
{
    Task<IEnumerable<Image>> GetAllByProductIdAsync(long productId, CancellationToken cancellationToken = default);
    Task<Image?> GetByIdAsync(long id, CancellationToken cancellationToken = default);
    void Insert(Image image);
    void Remove(Image image);
}
