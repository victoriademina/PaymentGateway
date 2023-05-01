using MediatR;
using Serilog;
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
        Log.Information("Handling request to add new transaction to database");
        
        var merchant = await _repository.GetMerchantById(request.MerchantId);
        Log.Information("Retrieved merchant with ID: {Id}", request.MerchantId);
        
        var transaction = await _repository.CreateTransaction(merchant, request.CardDetails.CardNumber);
        Log.Information("Added transaction in database");
        
        var bankResponse = await _bankAdapter.ProcessPayment(new BankRequest(transaction.Id, request.CardDetails, request.PaymentAmount));
        Log.Information("Processed payment with Transaction ID {TransactionId}", transaction.Id);
        
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