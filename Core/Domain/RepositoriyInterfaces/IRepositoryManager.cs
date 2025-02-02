using Domain.Repositories;

namespace Domain.RepositoriyInterfaces;

public interface IRepositoryManager
{
    IProductRepository ProductRepository { get; }
    IUserRepository UserRepository { get; }
    ICategoryRepository CategoryRepository { get; }
    IProductVariantRepository ProductVariantRepository { get; }
    IImageRepository ImageRepository { get; }
    IOrderRepository OrderRepository { get; }
    ICartRepository CartRepository { get; }
    ICartItemRepository CartItemRepository { get; }
    IPaymentRepository PaymentRepository { get; }
    IReviewRepository ReviewRepository { get; }
    IUnitOfWork UnitOfWork { get; }
}