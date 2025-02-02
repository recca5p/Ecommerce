using Domain.Repositories;

namespace Domain.RepositoriyInterfaces;

public interface IRepositoryManager
{
    IProductRepository ProductRepository { get; }
    IUserRepository UserRepository { get; }
    ICategoryRepository CategoryRepository { get; }
    IProductVariantRepository ProductVariantRepository { get; }
    IImageRepository ImageRepository { get; }
    IUnitOfWork UnitOfWork { get; }
}