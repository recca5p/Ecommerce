using Domain.Exceptions.Base;

namespace Domain.Exceptions;

public sealed class UserNotFoundException : NotFoundException
{
    public UserNotFoundException(long userId)
        : base($"The user with the identifier {userId} was not found.")
    {
    }
}
