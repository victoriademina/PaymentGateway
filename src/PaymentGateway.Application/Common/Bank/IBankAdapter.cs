namespace PaymentGateway.Application.Common.Bank;

public interface IBankAdapter
{
    public Task<BankResponse> ProcessPayment(BankRequest bankRequest);
}