using Serilog;

namespace BankSimulator.Sdk;

/// <summary>
/// Simulates SDK of one of the banks Payment Gateway integrated with.
/// </summary>
public class UltimateBankClient
{
    /// <summary>
    /// A list of cards transactions from which will be allowed.
    /// </summary>
    private static readonly List<string> CardsAllowList = new List<string>
    {
        "1111 1111 1111 1111",
        "2222 2222 2222 2222",
        "3333 3333 3333 3333"
    };
    
    /// <summary>
    /// A list of cards transactions from which will be denied.
    /// </summary>
    private static readonly List<string> CardsDenyList = new List<string>
    {
        "7777 7777 7777 7777",
        "8888 8888 8888 8888",
        "9999 9999 9999 9999"
    };
    
    public ClientResponse PayByCard(
        Guid paymentId,
        string cardNumber,
        string cvv,
        int expiryMonth,
        int expiryYear,
        string owner,
        decimal amount,
        string currency)
    {
        if (CardsAllowList.Contains(cardNumber))
        {
            Log.Information("The card is in Allow list, payment successful");
            return new ClientResponse(paymentId, 0, null);
        }

        if (CardsDenyList.Contains(cardNumber))
        {
            Log.Information("The card is in Deny list, payment denied");
            return new ClientResponse(paymentId, 1, "Your card was denied.");
        }
        // If the card number is not in Allow nor in Deny list, the transaction will be denied with Internal Error status.
        Log.Information("Internal error");
        return new ClientResponse(paymentId, 2, "Internal error, please try later.");
    }
}