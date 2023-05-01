namespace PaymentGateway.Application.Queries.GetTransaction;

/// <summary>
/// Exception that is thrown when a merchant is asking to retrieve a transaction that does not belong to them.
/// </summary>
public class GetTransactionAuthException : Exception
{
    public GetTransactionAuthException()
    {
    }

    public GetTransactionAuthException(string message)
        : base(message)
    {
    }
}