using MediatR;
using PaymentGateway.Domain.Entities;

namespace PaymentGateway.Application.Commands.CreateTransaction;

/// <summary>
/// Represents a request to create a new transaction.
/// </summary>
public class CreateTransactionRequest : IRequest<CreateTransactionResponse>
{
    /// <summary>
    /// Unique identifier of the merchant.
    /// </summary>
    public Guid MerchantId { get; set; }
    
    /// <summary>
    /// Represents the details of a payment card.
    /// </summary>
    public CardDetails CardDetails { get; set; }
    
    /// <summary>
    /// Represents amount and currency of the payment.
    /// </summary>
    public PaymentAmount PaymentAmount { get; set; }
}