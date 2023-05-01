using MediatR;
using PaymentGateway.Application.Common.Bank;
using PaymentGateway.Application.Common.Repository;
using PaymentGateway.Domain.Enums;

namespace PaymentGateway.Application.Commands.CreateTransaction;

public class CreateTransactionHandler : IRequestHandler<CreateTransactionRequest, Status>
{
    private readonly IPaymentGatewayRepository _repository;
    private readonly IBankAdapter _bankAdapter;

    public CreateTransactionHandler(IPaymentGatewayRepository repository, IBankAdapter bankAdapter)
    {
        _repository = repository;
        _bankAdapter = bankAdapter;
    }
    public Task<Status> Handle(CreateTransactionRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}