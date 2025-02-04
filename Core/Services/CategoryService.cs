using AutoMapper;
using Contract.DTOs.Request;
using Contract.DTOs.Response;
using Domain.Entities;
using Domain.Exceptions;
using Domain.RepositoriyInterfaces;
using Services.Abstraction;

namespace Services;

internal sealed class CategoryService : ICategoryService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IMapper _mapper;

    public CategoryService(IRepositoryManager repositoryManager, IMapper mapper)
    {
        _repositoryManager = repositoryManager;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CategoryDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var categories = await _repositoryManager.CategoryRepository.GetAllAsync(cancellationToken);
        return _mapper.Map<IEnumerable<CategoryDto>>(categories);
    }

    public async Task<CategoryDto> GetByIdAsync(long ID, CancellationToken cancellationToken = default)
    {
        var category = await _repositoryManager.CategoryRepository.GetByIdAsync(ID, cancellationToken);
        if (category == null)
        {
            throw new CategoryNotFoundException(ID);
        }
        return _mapper.Map<CategoryDto>(category);
    }

    public async Task CreateAsync(CategoryForCreationDto categoryRequest, CancellationToken cancellationToken = default)
    {
        var category = _mapper.Map<Category>(categoryRequest);
        category.CreatedDate = DateTime.UtcNow;
        category.CreatedById = 123;
        _repositoryManager.CategoryRepository.Insert(category);
        await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(long ID, CategoryForUpdateDto categoryRequest, CancellationToken cancellationToken = default)
    {
        var category = await _repositoryManager.CategoryRepository.GetByIdAsync(ID, cancellationToken);
        if (category == null)
        {
            throw new CategoryNotFoundException(ID);
        }
        category.CategoryName = categoryRequest.CategoryName;
        await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(long ID, CancellationToken cancellationToken = default)
    {
        var category = await _repositoryManager.CategoryRepository.GetByIdAsync(ID, cancellationToken);
        if (category == null)
        {
            throw new CategoryNotFoundException(ID);
        }
        _repositoryManager.CategoryRepository.Remove(category);
        await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
    }
}
