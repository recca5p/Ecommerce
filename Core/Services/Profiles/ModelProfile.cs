using AutoMapper;
using Contract.DTOs.Response;
using Domain.Entities;

namespace Services.Profiles;

public class ModelProfile : Profile
{
    public ModelProfile()
    {
        #region User

        CreateMap<Product, ProductDto>();

        #endregion
    }
}