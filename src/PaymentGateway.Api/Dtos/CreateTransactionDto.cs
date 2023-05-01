using PaymentGateway.Domain.Entities;

namespace PaymentGateway.Api.Dtos;

public record CreateTransactionDto(
    Guid MerchantId,
    CardDetails CardDetails,
    PaymentAmount PaymentAmount);