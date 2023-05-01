using PaymentGateway.Domain.Entities;

namespace PaymentGateway.Application.Common.Bank;

public record BankRequest(
    Guid TransactionId,
    CardDetails CardDetails,
    PaymentAmount PaymentAmount);