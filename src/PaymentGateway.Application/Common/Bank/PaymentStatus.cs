namespace PaymentGateway.Application.Common.Bank;

/// <summary>
/// Defines possible payment statuses.
/// </summary>
public enum PaymentStatus
{
    Success,
    Failure,
    InternalError
}