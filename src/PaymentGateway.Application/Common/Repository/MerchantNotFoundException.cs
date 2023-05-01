namespace PaymentGateway.Application.Common.Repository;

/// <summary>
/// Exception that is thrown when a merchant is not found in the repository.
/// </summary>
public class MerchantNotFoundException : Exception
{
    public MerchantNotFoundException()
    {
    }

    public MerchantNotFoundException(string message)
        : base(message)
    {
    }
}