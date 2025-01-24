using Contract.DTOs.Request;
using Contract.DTOs.Response;
using Domain.Entities;

namespace Services.Abstraction;

public interface IUserService
{
    Task<IEnumerable<UserDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<UserDto> GetByIdAsync(long ID, CancellationToken cancellationToken = default); 
    Task<UserDto> CreateAsync(UserForCreationDto userForCreationDto, CancellationToken cancellationToken = default);

    Task UpdateAsync(long ID, UserForUpdateDto userForUpdateDto, CancellationToken cancellationToken = default);
    Task DeleteAsync(long ID, CancellationToken cancellationToken = default);
}
