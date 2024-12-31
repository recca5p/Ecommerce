using Domain.Repositories;

namespace Domain.RepositoriyInterfaces;

public interface IRepositoryManager
{
    IProductRepository ProductRepository { get; }
    IUnitOfWork UnitOfWork { get; }
}