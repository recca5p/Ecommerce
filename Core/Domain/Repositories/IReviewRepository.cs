using Domain.Entities;

namespace Domain.RepositoriyInterfaces;

public interface IReviewRepository
{
    Task<IEnumerable<Review>> GetAllByProductIdAsync(long productId, CancellationToken cancellationToken = default);
    void Insert(Review review);
}
