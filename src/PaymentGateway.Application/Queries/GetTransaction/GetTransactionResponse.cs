using PaymentGateway.Domain.Enums;

namespace PaymentGateway.Application.Queries.GetTransaction;

public class GetTransactionResponse
{
    public Guid TransactionId { get; set; }
    public Status Status { get; set; }
}