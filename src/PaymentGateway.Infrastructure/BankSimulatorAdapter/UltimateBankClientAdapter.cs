using BankSimulator.Sdk;
using PaymentGateway.Application.Common.Bank;

namespace PaymentGateway.Infrastructure.BankSimulatorAdapter;

public class UltimateBankClientAdapter : IBankAdapter
{
    private readonly UltimateBankClient _bankClient;

    public UltimateBankClientAdapter(UltimateBankClient bankClient)
    {
        _bankClient = bankClient;
    }
    public Task<BankResponse> ProcessPayment(BankRequest bankRequest)
    {
        var response = _bankClient.PayByCard(
            bankRequest.PaymentId,
            bankRequest.CardDetails.CardNumber,
            bankRequest.CardDetails.Cvv,
            bankRequest.CardDetails.ExpiryMonth,
            bankRequest.CardDetails.ExpiryYear,
            bankRequest.CardDetails.Owner,
            bankRequest.PaymentAmount.Amount,
            bankRequest.PaymentAmount.Currency
        );
        PaymentStatus status = response.StatusCode switch
        {
            0 => PaymentStatus.Success,
            1 => PaymentStatus.Failure,
            _ => PaymentStatus.InternalError
        };
        return Task.FromResult(new BankResponse(
            response.PaymentId,
            status,
            response.Reason));
    }
}