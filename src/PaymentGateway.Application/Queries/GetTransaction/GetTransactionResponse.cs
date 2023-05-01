using PaymentGateway.Domain.Enums;

namespace PaymentGateway.Application.Queries.GetTransaction;

/// <summary>
/// Represents a response of getting transaction details.
/// </summary>
public class GetTransactionResponse
{
    /// <summary>
    /// ID of the transaction.
    /// </summary>
    public Guid TransactionId { get; set; }
    
    /// <summary>
    /// Status of the transaction.
    /// </summary>
    public Status Status { get; set; }
}