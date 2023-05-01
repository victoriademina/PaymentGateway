namespace PaymentGateway.Application.Common.Repository;

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