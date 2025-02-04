using AutoMapper;
using Contract.DTOs.Request;
using Contract.DTOs.Response;
using Domain.Entities;

namespace Services.Profiles;

public class ModelProfile : Profile
{
    public ModelProfile()
    {
        #region Product

        CreateMap<User, UserDto>();
        CreateMap<UserForCreationDto, User>();
        CreateMap<UserForUpdateDto, User>();

        #endregion

        #region Product

        CreateMap<Product, ProductDto>();
        CreateMap<Product, ProductDetailDto>();
        CreateMap<ProductForCreationDto, Product>();
        CreateMap<ProductForUpdateDto, Product>();

        #endregion

        #region ProductVariant

        CreateMap<ProductVariant, ProductVariantDto>();
        CreateMap<ProductVariantForCreationDto, ProductVariant>();
        CreateMap<ProductVariantForUpdateDto, ProductVariant>();

        #endregion

        #region Category

        CreateMap<Category, CategoryDto>();
        CreateMap<CategoryForCreationDto, Category>();
        CreateMap<CategoryForUpdateDto, Category>();

        #endregion

        #region Image

        CreateMap<Image, ImageDto>();
        CreateMap<ImageForCreationDto, Image>();

        #endregion

        #region Order

        CreateMap<Order, OrderDto>();
        CreateMap<OrderDetail, OrderDetailDto>();
        CreateMap<OrderForCreationDto, Order>();
        CreateMap<OrderDetailForCreationDto, OrderDetail>();
        CreateMap<OrderForUpdateDto, Order>();

        #endregion

        #region Cart

        CreateMap<Cart, CartDto>();
        CreateMap<CartItem, CartItemDto>();
        CreateMap<CartItemForCreationDto, CartItem>();
        CreateMap<CartItemForUpdateDto, CartItem>();

        #endregion

        #region Review

        CreateMap<Review, ReviewDto>();
        CreateMap<ReviewForCreationDto, Review>();

        #endregion

        #region Payment

        CreateMap<Payment, PaymentDto>();
        CreateMap<PaymentForCreationDto, Payment>();

        #endregion

        #region MyRegion
        
        CreateMap<CategoryForCreationDto, Category>();
        
        #endregion
    }
}
