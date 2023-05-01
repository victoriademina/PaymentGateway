using MediatR;

namespace PaymentGateway.Application.Queries.GetTransaction;

/// <summary>
/// Represents a request to get details of a transaction.
/// </summary>
public class GetTransactionRequest : IRequest<GetTransactionResponse>
{
    /// <summary>
    /// ID of the transaction.
    /// </summary>
    public Guid TransactionId { get; set; }
    
    /// <summary>
    /// ID of the merchant.
    /// </summary>
    public Guid MerchantId { get; set; }
}