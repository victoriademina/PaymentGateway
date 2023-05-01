using MediatR;
using PaymentGateway.Application.Common.Repository;
using Serilog;

namespace PaymentGateway.Application.Commands.CreateMerchant;

/// <summary>
/// Handles the creation of a new merchant.
/// </summary>
public class CreateMerchantHandler : IRequestHandler<CreateMerchantRequest, CreateMerchantResponse>
{
    private readonly IPaymentGatewayRepository _repository;
    
    public CreateMerchantHandler(IPaymentGatewayRepository repository)
    {
        _repository = repository;
    }
    
    /// <summary>
    /// Handles the creation of a new merchant.
    /// </summary>
    /// <param name="request">The request to create a new merchant.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the operation.</param>
    /// <returns>A response indicating the ID of the newly created merchant.</returns>
    public async Task<CreateMerchantResponse> Handle(CreateMerchantRequest request, CancellationToken cancellationToken)
    {
        Log.Information("Handling request to add new merchant to database");
        
        var merchant = await _repository.AddMerchant();
        return new CreateMerchantResponse{MerchantId = merchant.Id};
    }
}