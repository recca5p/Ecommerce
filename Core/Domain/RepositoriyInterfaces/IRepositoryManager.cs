using Domain.Repositories;

namespace Domain.RepositoriyInterfaces;

public interface IRepositoryManager
{
    IProductRepository ProductRepository { get; }
    IUserRepository UserRepository { get; }
    IUnitOfWork UnitOfWork { get; }
}