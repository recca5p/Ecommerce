﻿using AutoMapper;
using Contract;
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

    public async Task<IEnumerable<OrderDto>> GetAll(CancellationToken cancellationToken = default)
    {
        IEnumerable<Order> orders;
        if (TokenExtension.GetRole())
            orders = await _repositoryManager.OrderRepository.GetAll(cancellationToken);
        else
            orders = await _repositoryManager.OrderRepository.GetAllByUserIdAsync(long.Parse(TokenExtension.GetUserId()), cancellationToken);

        return _mapper.Map<IEnumerable<OrderDto>>(orders);
    }

    public async Task<OrderDetailDto> GetOrderDetailsAsync(long id, CancellationToken cancellationToken = default)
    {
        var order = await _repositoryManager.OrderRepository.GetByIdWithDetailsAsync(id, cancellationToken);
        return _mapper.Map<OrderDetailDto>(order);
    }

    public async Task<OrderDto> CreateAsync(OrderForCreationDto orderDto, CancellationToken cancellationToken = default)
    {
        User user = await _repositoryManager.UserRepository.GetByIdAsync(orderDto.UserId, cancellationToken);
        
        if (user is null)
            throw new Exception($"User with id {orderDto.UserId} does not exist");

        IList<OrderDetail> orderDetails = new List<OrderDetail>();

        foreach (var orderDetailDto in orderDto.OrderDetails)
        {
            ProductVariant productVariant = await _repositoryManager.ProductVariantRepository.GetByIdAsync(orderDetailDto.VariantId, cancellationToken);
            
            if (productVariant == null)
                throw new Exception($"Product variant with id {orderDetailDto.VariantId} does not exist");
            
            if (productVariant.Stock < orderDetailDto.Quantity)
                throw new Exception($"Product variant with id {orderDetailDto.VariantId} does not have enough stock");
            
            var orderDetail = _mapper.Map<OrderDetail>(orderDetailDto);

            orderDetail.Price = productVariant.Price * orderDetailDto.Quantity;
            
            productVariant.Stock -= orderDetail.Quantity;

            _repositoryManager.ProductVariantRepository.Update(productVariant);
            
            orderDetails.Add(orderDetail);
        }
        
        var order = _mapper.Map<Order>(orderDto);
        order.TotalPrice = orderDetails.Sum(x => x.Price);
        order.ShippingStatus = string.Empty;
        order.PaymentStatus = string.Empty;
        order.OrderDetails = orderDetails;
        order.OrderDate = DateTime.UtcNow;
        
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
