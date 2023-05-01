namespace PaymentGateway.Application.Queries.GetTransaction;

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