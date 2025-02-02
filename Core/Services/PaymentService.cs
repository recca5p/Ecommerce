using AutoMapper;
using Contract.DTOs.Request;
using Contract.DTOs.Response;
using Domain.Entities;
using Domain.RepositoriyInterfaces;
using Services.Abstraction;

namespace Services;

internal sealed class PaymentService : IPaymentService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IMapper _mapper;

    public PaymentService(IRepositoryManager repositoryManager, IMapper mapper)
    {
        _repositoryManager = repositoryManager;
        _mapper = mapper;
    }

    public async Task<IEnumerable<PaymentDto>> GetAllByUserIdAsync(long userId, CancellationToken cancellationToken = default)
    {
        var payments = await _repositoryManager.PaymentRepository.GetAllByUserIdAsync(userId, cancellationToken);
        return _mapper.Map<IEnumerable<PaymentDto>>(payments);
    }

    public async Task<PaymentDto> GetByIdAsync(long paymentId, CancellationToken cancellationToken = default)
    {
        var payment = await _repositoryManager.PaymentRepository.GetByIdAsync(paymentId, cancellationToken);
        return _mapper.Map<PaymentDto>(payment);
    }

    public async Task CreateAsync(PaymentForCreationDto paymentDto, CancellationToken cancellationToken = default)
    {
        var payment = _mapper.Map<Payment>(paymentDto);
        _repositoryManager.PaymentRepository.Insert(payment);
        await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(long paymentId, PaymentForUpdateDto paymentDto, CancellationToken cancellationToken = default)
    {
        var payment = await _repositoryManager.PaymentRepository.GetByIdAsync(paymentId, cancellationToken);
        if (payment == null)
        {
            throw new Exception($"Payment with ID {paymentId} not found.");
        }

        _mapper.Map(paymentDto, payment);
        await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(long paymentId, CancellationToken cancellationToken = default)
    {
        var payment = await _repositoryManager.PaymentRepository.GetByIdAsync(paymentId, cancellationToken);
        if (payment == null)
        {
            throw new Exception($"Payment with ID {paymentId} not found.");
        }

        _repositoryManager.PaymentRepository.Remove(payment);
        await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
    }
}
