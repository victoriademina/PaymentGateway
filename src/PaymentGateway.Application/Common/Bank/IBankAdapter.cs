namespace PaymentGateway.Application.Common.Bank;

public interface IBankAdapter
{
    public Task<BankResponse> ProcessPayment(BankRequest bankRequest);
}

// sdk in bank simulator
// then class super-duper bank client