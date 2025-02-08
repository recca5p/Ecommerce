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
        var variant = await _repositoryManager.ProductVariantRepository.GetByIdAsync(cartItemDto.VariantId, cancellationToken);
        
        if (variant == null)
            throw new Exception("Variant not found");
        
        var cart = await _repositoryManager.CartRepository.GetByUserIdAsync(userId, cancellationToken);
        var cartItem = _mapper.Map<CartItem>(cartItemDto);

        if (cart == null)
        {

            cart = new Cart
            {
                UserId = userId, 
                TotalPrice = variant.Price * cartItemDto.Quantity,
                CartItems = new List<CartItem>(){cartItem},
            };
            _repositoryManager.CartRepository.Insert(cart);
        }
        else
        {
            cart.TotalPrice += (variant.Price * cartItemDto.Quantity);

            cart.CartItems.Add(cartItem);

            _repositoryManager.CartRepository.Update(cart);
        }
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
