using Domain.Exceptions.Base;

namespace Domain.Exceptions;

public sealed class CategoryNotFoundException : NotFoundException
{
    public CategoryNotFoundException(long categoryId)
        : base($"The category with the identifier {categoryId} was not found.")
    {
    }
}
