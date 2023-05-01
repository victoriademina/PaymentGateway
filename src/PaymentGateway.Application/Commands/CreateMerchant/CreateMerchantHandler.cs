using MediatR;
using PaymentGateway.Application.Common.Repository;

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
        var merchant = await _repository.AddMerchant();
        return new CreateMerchantResponse{MerchantId = merchant.Id};
    }
}