using MediatR;
using Microsoft.AspNetCore.Mvc;
using PaymentGateway.Application.Commands.CreateMerchant;

namespace PaymentGateway.Api.Controllers;

[ApiController]
[Route("merchants")]
public class MerchantsController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public MerchantsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost("create")]
    public async Task<ActionResult<CreateMerchantResponse>> CreateMerchant()
    {
        var response = await _mediator.Send(new CreateMerchantRequest());
        return response;
    }
}