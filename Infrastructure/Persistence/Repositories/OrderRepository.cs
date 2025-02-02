using Domain.Entities;
using Domain.RepositoriyInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

internal sealed class OrderRepository : IOrderRepository
{
    private readonly IRepositoryDbContext _dbContext;

    public OrderRepository(IRepositoryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Order>> GetAllByUserIdAsync(long userId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Orders.Where(o => o.UserId == userId && !o.IsDeleted).ToListAsync(cancellationToken);
    }

    public async Task<Order?> GetByIdWithDetailsAsync(long id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Orders
            .Include(o => o.OrderDetails)
            .FirstOrDefaultAsync(o => o.OrderId == id && !o.IsDeleted, cancellationToken);
    }

    public async Task<Order?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Orders.FirstOrDefaultAsync(o => o.OrderId == id && !o.IsDeleted, cancellationToken);
    }

    public void Insert(Order order)
    {
        _dbContext.Orders.Add(order);
    }

    public void Remove(Order order)
    {
        order.IsDeleted = true;
        _dbContext.Orders.Update(order);
    }
}
