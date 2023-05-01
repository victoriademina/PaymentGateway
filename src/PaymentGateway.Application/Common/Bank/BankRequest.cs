using PaymentGateway.Domain.Entities;

namespace PaymentGateway.Application.Common.Bank;

/// <summary>
/// Represents a request to the bank for processing a payment.
/// </summary>
public record BankRequest(
    Guid TransactionId,
    CardDetails CardDetails,
    PaymentAmount PaymentAmount);