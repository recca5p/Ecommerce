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

        CreateMap<Product, ProductDto>();

        #endregion

        #region MyRegion
        
        CreateMap<CategoryForCreationDto, Category>();
        
        #endregion
    }
}