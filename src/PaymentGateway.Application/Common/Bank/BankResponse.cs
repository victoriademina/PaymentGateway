namespace PaymentGateway.Application.Common.Bank;

public record BankResponse(
    Guid PaymentId,
    PaymentStatus Status,
    string? Reason
);