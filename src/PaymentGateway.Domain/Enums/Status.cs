namespace PaymentGateway.Domain.Enums;

/// <summary>
/// Represents the status of a payment transaction: Pending, Success, or Failed
/// </summary>
public enum Status
{
    Pending = 0,
    Success = 1, 
    Failed = 2
}