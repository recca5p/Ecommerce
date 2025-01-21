using Domain.Exceptions.Base;

namespace Domain.Exceptions;

public sealed class CategoryNotFoundException: NotFoundException
{
    public CategoryNotFoundException(long categoryID)
        : base($"The category with the identifier {categoryID} was not found.")    
    {
    }
}