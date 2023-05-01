namespace PaymentGateway.Application.Commands.CreateMerchant;

/// <summary>
/// Represents the CreateMerchantResponse containing the ID of the created merchant.
/// </summary>
public class CreateMerchantResponse
{
    public Guid MerchantId { get; set; }
}