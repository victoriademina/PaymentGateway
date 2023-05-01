using MediatR;
using PaymentGateway.Domain.Entities;
using PaymentGateway.Domain.Enums;

namespace PaymentGateway.Application.Commands.CreateTransaction;

public class CreateTransactionRequest : IRequest<CreateTransactionResponse>
{
    public Guid TransactionId { get; set; }
    public Guid MerchantId { get; set; }
    public CardDetails CardDetails { get; set; }
    public PaymentAmount PaymentAmount { get; set; }
}