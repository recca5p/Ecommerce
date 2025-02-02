namespace Services.Abstraction;

public interface IServiceManager
{
    IProductService ProductService { get; }
    IUserService UserService { get; }
    ICategoryService CategoryService { get; }
    IProductVariantService ProductVariantService { get; }
    IImageService ImageService { get; }
}