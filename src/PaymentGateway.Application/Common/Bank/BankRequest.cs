using PaymentGateway.Domain.Entities;

namespace PaymentGateway.Application.Common.Bank;

public record BankRequest(
    Guid PaymentId,
    CardDetails CardDetails,
    PaymentAmount PaymentAmount);