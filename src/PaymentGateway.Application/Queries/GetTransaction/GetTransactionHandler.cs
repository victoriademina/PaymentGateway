using MediatR;
using PaymentGateway.Application.Common.Repository;

namespace PaymentGateway.Application.Queries.GetTransaction;

/// <summary>
/// Handles the <see cref="GetTransactionRequest"/> and returns the corresponding <see cref="GetTransactionResponse"/>.
/// </summary>
public class GetTransactionHandler : IRequestHandler<GetTransactionRequest, GetTransactionResponse>
{
    private readonly IPaymentGatewayRepository _repository;
    
    public GetTransactionHandler(IPaymentGatewayRepository repository)
    {
        _repository = repository;
    }
    public async Task<GetTransactionResponse> Handle(GetTransactionRequest request, CancellationToken cancellationToken)
    {
        var transaction = await _repository.GetTransactionById(request.TransactionId);
        if (request.MerchantId != transaction.MerchantId)
        {
            throw new GetTransactionAuthException("You do not have access to this transaction.");
        }
        return new GetTransactionResponse
        {
            TransactionId = transaction.Id,
            Status = transaction.Status
        };
    }
}