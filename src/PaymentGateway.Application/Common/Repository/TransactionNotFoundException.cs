namespace PaymentGateway.Application.Common.Repository;

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