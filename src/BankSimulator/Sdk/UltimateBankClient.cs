namespace BankSimulator.Sdk;

public class UltimateBankClient
{
    private static readonly List<string> CardsAllowList = new List<string>
    {
        "1111 1111 1111 1111",
        "2222 2222 2222 2222",
        "3333 3333 3333 3333"
    };

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
            return new ClientResponse(paymentId, 0, null);
        }

        if (CardsDenyList.Contains(cardNumber))
        {
            return new ClientResponse(paymentId, 1, "Your card was denied.");
        }

        return new ClientResponse(paymentId, 2, "Internal error, please try later.");
    }
}