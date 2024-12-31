using Domain.Exceptions.Base;

namespace Domain.Exceptions;

public sealed class ProductDoesNotBelongToCategoryException : BadRequestException
{
    public ProductDoesNotBelongToCategoryException(long productID, long categoryID)
        : base($"The product with the identifier {productID} does not belong to the category with the identifier {categoryID}")
    {
    }
}