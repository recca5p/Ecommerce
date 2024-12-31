using Domain.Exceptions.Base;

namespace Domain.Exceptions;

public sealed class ProductNotFoundException : NotFoundException
{
    public ProductNotFoundException(long productID)
        : base($"The product with the identifier {productID} was not found.")
    {
    }
}