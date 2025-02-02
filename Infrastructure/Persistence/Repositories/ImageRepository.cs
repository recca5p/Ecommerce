using Domain.Entities;
using Domain.RepositoriyInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

internal sealed class ImageRepository : IImageRepository
{
    private readonly IRepositoryDbContext _dbContext;

    public ImageRepository(IRepositoryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Image>> GetAllByProductIdAsync(long productId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Images.Where(i => i.ProductId == productId && !i.IsDeleted).ToListAsync(cancellationToken);
    }

    public async Task<Image?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Images.FirstOrDefaultAsync(i => i.ImageId == id && !i.IsDeleted, cancellationToken);
    }

    public void Insert(Image image)
    {
        _dbContext.Images.Add(image);
    }

    public void Remove(Image image)
    {
        image.IsDeleted = true;
        _dbContext.Images.Update(image);
    }
}
