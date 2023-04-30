namespace PaymentGateway.Application.Common.Exceptions;

public class TransactionNotFoundException : Exception
{
    public TransactionNotFoundException()
    {
    }

    public TransactionNotFoundException(string message)
        : base(message)
    {
    }
}