using AutoMapper;
using Contract.DTOs.Request;
using Contract.DTOs.Response;
using Domain.Entities;
using Domain.RepositoriyInterfaces;
using Services.Abstraction;

namespace Services;

internal sealed class OrderService : IOrderService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IMapper _mapper;

    public OrderService(IRepositoryManager repositoryManager, IMapper mapper)
    {
        _repositoryManager = repositoryManager;
        _mapper = mapper;
    }

    public async Task<IEnumerable<OrderDto>> GetAllByUserIdAsync(long userId, CancellationToken cancellationToken = default)
    {
        var orders = await _repositoryManager.OrderRepository.GetAllByUserIdAsync(userId, cancellationToken);
        return _mapper.Map<IEnumerable<OrderDto>>(orders);
    }

    public async Task<OrderDetailDto> GetOrderDetailsAsync(long id, CancellationToken cancellationToken = default)
    {
        var order = await _repositoryManager.OrderRepository.GetByIdWithDetailsAsync(id, cancellationToken);
        return _mapper.Map<OrderDetailDto>(order);
    }

    public async Task<OrderDto> CreateAsync(OrderForCreationDto orderDto, CancellationToken cancellationToken = default)
    {
        var order = _mapper.Map<Order>(orderDto);
        _repositoryManager.OrderRepository.Insert(order);
        await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
        return _mapper.Map<OrderDto>(order);
    }

    public async Task UpdateAsync(long id, OrderForUpdateDto orderDto, CancellationToken cancellationToken = default)
    {
        var order = await _repositoryManager.OrderRepository.GetByIdAsync(id, cancellationToken);
        if (order == null)
        {
            throw new Exception($"Order with ID {id} not found.");
        }

        _mapper.Map(orderDto, order);
        await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(long id, CancellationToken cancellationToken = default)
    {
        var order = await _repositoryManager.OrderRepository.GetByIdAsync(id, cancellationToken);
        if (order == null)
        {
            throw new Exception($"Order with ID {id} not found.");
        }

        _repositoryManager.OrderRepository.Remove(order);
        await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
    }
}
