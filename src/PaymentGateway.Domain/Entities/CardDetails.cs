namespace PaymentGateway.Domain.Entities;

/// <summary>
/// Represents the details of a payment card.
/// </summary>
public class CardDetails
{
    /// <summary>
    /// Card number.
    /// </summary>
    /// <remarks>
    /// The card number is a string of digits.
    /// </remarks>
    public string CardNumber { get; set; }
    
    /// <summary>
    /// Card verification value (CVV).
    /// </summary>
    /// <remarks>
    /// The CVV is a three- or four-digit code printed on the back of the card, used for security purposes.
    /// </remarks>
    public string Cvv { get; set; }
    
    /// <summary>
    /// Expiry month of the card.
    /// </summary>
    /// <remarks>
    /// The expiry month is a number between 1 and 12.
    /// </remarks>
    public int ExpiryMonth { get; set; }
    
    /// <summary>
    /// Expiry year of the card.
    /// </summary>
    /// <remarks>
    /// The expiry year is a four-digit number representing the year the card expires.
    /// </remarks>
    public int ExpiryYear { get; set; }
    
    /// <summary>
    /// Name of the card owner.
    /// </summary>
    public string Owner { get; set; }
}