using MediatR;
using PaymentGateway.Application.Common.Bank;
using PaymentGateway.Application.Common.Repository;
using PaymentGateway.Domain.Enums;

namespace PaymentGateway.Application.Commands.CreateTransaction;

public class CreateTransactionHandler : IRequestHandler<CreateTransactionRequest, CreateTransactionResponse>
{
    private readonly IPaymentGatewayRepository _repository;
    private readonly IBankAdapter _bankAdapter;

    public CreateTransactionHandler(IPaymentGatewayRepository repository, IBankAdapter bankAdapter)
    {
        _repository = repository;
        _bankAdapter = bankAdapter;
    }
    public async Task<CreateTransactionResponse> Handle(CreateTransactionRequest request, CancellationToken cancellationToken)
    {
        var merchant = await _repository.GetMerchantById(request.MerchantId);
        var transaction = await _repository.CreateTransaction(merchant, request.CardDetails.CardNumber);
        var bankResponse = await _bankAdapter.ProcessPayment(new BankRequest(transaction.Id, request.CardDetails, request.PaymentAmount));
        var status = bankResponse.Status switch
        {
            PaymentStatus.Success => Status.Success,
            _ => Status.Failed
        };
        await _repository.UpdateTransactionStatus(transaction.Id, status);
        return new CreateTransactionResponse
        {
            TransactionId = transaction.Id,
            Status = status
        };
    }
}