namespace BankSimulator.Sdk;

public record ClientResponse(
    Guid PaymentId,
    int StatusCode, //0 - success, 1 - failure, everything else - internal error
    string? Reason
);