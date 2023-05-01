namespace PaymentGateway.Application.Common.Bank;

/// <summary>
/// Interface for processing payments through a bank.
/// </summary>
public interface IBankAdapter
{
    /// <summary>
    /// Processes a payment request through a bank.
    /// </summary>
    /// <param name="bankRequest">The request containing transaction, card and payment details.</param>
    /// <returns>A response from the bank containing status and additional information about the transaction.</returns>
    public Task<BankResponse> ProcessPayment(BankRequest bankRequest);
}