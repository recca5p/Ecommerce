using AutoMapper;
using Contract.DTOs.Request;
using Contract.DTOs.Response;
using Domain.Entities;
using Domain.Exceptions;
using Domain.RepositoriyInterfaces;
using Services.Abstraction;

namespace Services;

internal sealed class UserService : IUserService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IMapper _mapper;

    public UserService(IRepositoryManager repositoryManager, IMapper mapper)
    {
        _repositoryManager = repositoryManager;
        _mapper = mapper;
    }

    public async Task<IEnumerable<UserDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var users = await _repositoryManager.UserRepository.GetAllAsync(cancellationToken);
        return _mapper.Map<IEnumerable<UserDto>>(users);
    }

    public async Task<UserDto> GetByIdAsync(long ID, CancellationToken cancellationToken = default)
    {
        var user = await _repositoryManager.UserRepository.GetByIdAsync(ID, cancellationToken);
        if (user == null)
        {
            throw new UserNotFoundException(ID);
        }

        return _mapper.Map<UserDto>(user);
    }

    public async Task<UserDto> CreateAsync(UserForCreationDto userRequest, CancellationToken cancellationToken = default)
    {
        var user = _mapper.Map<User>(userRequest);

        _repositoryManager.UserRepository.Insert(user);
        await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);

        // Map the created user to a DTO and return it
        return _mapper.Map<UserDto>(user);
    }


    public async Task UpdateAsync(long ID, UserForUpdateDto userRequest, CancellationToken cancellationToken = default)
    {
        var user = await _repositoryManager.UserRepository.GetByIdAsync(ID, cancellationToken);
        if (user == null)
        {
            throw new UserNotFoundException(ID);
        }
        user.Name = userRequest.Name;
        user.Email = userRequest.Email;
        user.Password = userRequest.Password;
        await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(long ID, CancellationToken cancellationToken = default)
    {
        var user = await _repositoryManager.UserRepository.GetByIdAsync(ID, cancellationToken);
        if (user == null)
        {
            throw new UserNotFoundException(ID);
        }
        _repositoryManager.UserRepository.Remove(user);
        await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
    }
}
