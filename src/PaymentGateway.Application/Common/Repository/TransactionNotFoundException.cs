namespace PaymentGateway.Application.Common.Repository;

/// <summary>
/// Exception that is thrown when a transaction is not found in the repository.
/// </summary>
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