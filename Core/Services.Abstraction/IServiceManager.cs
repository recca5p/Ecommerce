namespace Services.Abstraction;

public interface IServiceManager
{
    IProductService ProductService { get; }
    IUserService UserService { get; }
    ICategoryService CategoryService { get; }
    IProductVariantService ProductVariantService { get; }
    IOrderService OrderService { get; }
    ICartService CartService { get; }
    IImageService ImageService { get; }
    IPaymentService PaymentService { get; }
    IReviewService ReviewService { get; }
}