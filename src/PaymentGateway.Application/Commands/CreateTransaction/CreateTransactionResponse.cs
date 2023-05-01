using PaymentGateway.Domain.Enums;

namespace PaymentGateway.Application.Commands.CreateTransaction;

public class CreateTransactionResponse
{
    public Guid TransactionId { get; set; }
    public Status Status { get; set; }
}