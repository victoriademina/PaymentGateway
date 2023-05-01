using MediatR;
using Serilog;
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
    
    /// <summary>
    /// Creates a new merchant.
    /// </summary>
    /// <returns>The response containing the new merchant ID.</returns>
    [HttpPost("create")]
    public async Task<ActionResult<CreateMerchantResponse>> CreateMerchant()
    {
        Log.Information("Creating a new merchant");
        
        var response = await _mediator.Send(new CreateMerchantRequest());
        Log.Information("Merchant with the following ID was created: {MerchantId}", response.MerchantId);
        return response;
    }
}