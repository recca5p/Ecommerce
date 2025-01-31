namespace Services.Abstraction;

public interface IServiceManager
{
    IProductService ProductService { get; }
    IUserService UserService { get; }
}