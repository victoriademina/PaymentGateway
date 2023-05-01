using PaymentGateway.Domain.Enums;

namespace PaymentGateway.Application.Commands.CreateTransaction;

/// <summary>
/// Represents the CreateTransactionResponse containing the ID of the created merchant and the transaction status.
/// </summary>
public class CreateTransactionResponse
{
    public Guid TransactionId { get; set; }
    public Status Status { get; set; }
}