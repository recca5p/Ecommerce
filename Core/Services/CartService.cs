using AutoMapper;
using Contract.DTOs.Request;
using Contract.DTOs.Response;
using Domain.Entities;
using Domain.RepositoriyInterfaces;
using Services.Abstraction;

namespace Services;

internal sealed class CartService : ICartService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IMapper _mapper;

    public CartService(IRepositoryManager repositoryManager, IMapper mapper)
    {
        _repositoryManager = repositoryManager;
        _mapper = mapper;
    }

    public async Task<CartDto> GetByUserIdAsync(long userId, CancellationToken cancellationToken = default)
    {
        var cart = await _repositoryManager.CartRepository.GetByUserIdAsync(userId, cancellationToken);
        return _mapper.Map<CartDto>(cart);
    }

    public async Task AddItemAsync(long userId, CartItemForCreationDto cartItemDto, CancellationToken cancellationToken = default)
    {
        var cart = await _repositoryManager.CartRepository.GetByUserIdAsync(userId, cancellationToken);
        if (cart == null)
        {
            cart = new Cart { UserId = userId };
            _repositoryManager.CartRepository.Insert(cart);
        }

        var cartItem = _mapper.Map<CartItem>(cartItemDto);
        cartItem.CartId = cart.CartId;

        _repositoryManager.CartItemRepository.Insert(cartItem);
        await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateItemAsync(long userId, long itemId, CartItemForUpdateDto cartItemDto, CancellationToken cancellationToken = default)
    {
        var cartItem = await _repositoryManager.CartItemRepository.GetByIdAsync(itemId, cancellationToken);
        if (cartItem == null || cartItem.Cart.UserId != userId)
        {
            throw new Exception("Cart item not found or does not belong to the user.");
        }

        _mapper.Map(cartItemDto, cartItem);
        await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveItemAsync(long userId, long itemId, CancellationToken cancellationToken = default)
    {
        var cartItem = await _repositoryManager.CartItemRepository.GetByIdAsync(itemId, cancellationToken);
        if (cartItem == null || cartItem.Cart.UserId != userId)
        {
            throw new Exception("Cart item not found or does not belong to the user.");
        }

        _repositoryManager.CartItemRepository.Remove(cartItem);
        await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
    }
}
