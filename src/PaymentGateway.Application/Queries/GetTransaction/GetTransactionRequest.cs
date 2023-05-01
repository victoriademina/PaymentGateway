using MediatR;
using PaymentGateway.Application.Commands.CreateTransaction;

namespace PaymentGateway.Application.Queries.GetTransaction;

public class GetTransactionRequest : IRequest<GetTransactionResponse>
{
    public Guid TransactionId { get; set; }
    public Guid MerchantId { get; set; }
}