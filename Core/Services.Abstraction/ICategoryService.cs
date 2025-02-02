using Contract.DTOs.Request;
using Contract.DTOs.Response;

namespace Services.Abstraction;

public interface ICategoryService
{
    Task<IEnumerable<CategoryDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<CategoryDto> GetByIdAsync(long ID, CancellationToken cancellationToken = default);
    Task CreateAsync(CategoryForCreationDto categoryForCreationDto, CancellationToken cancellationToken = default);
    Task UpdateAsync(long ID, CategoryForUpdateDto categoryForUpdateDto, CancellationToken cancellationToken = default);
    Task DeleteAsync(long ID, CancellationToken cancellationToken = default);
}
