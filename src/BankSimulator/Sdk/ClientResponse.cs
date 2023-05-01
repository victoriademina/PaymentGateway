namespace BankSimulator.Sdk;

/// <summary>
/// Represents a response from a bank client.
/// </summary>
public record ClientResponse(
    Guid PaymentId,
    int StatusCode, //0 - success, 1 - failure, everything else - internal error
    string? Reason
);