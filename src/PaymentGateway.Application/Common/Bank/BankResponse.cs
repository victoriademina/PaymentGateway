namespace PaymentGateway.Application.Common.Bank;

/// <summary>
/// Represents a response of the bank that processing a payment.
/// </summary>
public record BankResponse(
    Guid PaymentId,
    PaymentStatus Status,
    string? Reason
);