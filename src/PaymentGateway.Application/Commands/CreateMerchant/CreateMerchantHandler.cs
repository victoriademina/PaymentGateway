using System.Reflection.Metadata.Ecma335;
using MediatR;
using PaymentGateway.Application.Common.Repository;
using Serilog;

namespace PaymentGateway.Application.Commands.CreateMerchant;

public class CreateMerchantHandler : IRequestHandler<CreateMerchantRequest, CreateMerchantResponse>
{
    private readonly IPaymentGatewayRepository _repository;
    
    public CreateMerchantHandler(IPaymentGatewayRepository repository)
    {
        _repository = repository;
    }
    public async Task<CreateMerchantResponse> Handle(CreateMerchantRequest request, CancellationToken cancellationToken)
    {
        Log.Information("Handling request to add new merchant to database");
        
        var merchant = await _repository.AddMerchant();
        return new CreateMerchantResponse{MerchantId = merchant.Id};
    }
}