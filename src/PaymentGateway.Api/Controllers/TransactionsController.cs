using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PaymentGateway.Api.Dtos;
using PaymentGateway.Application.Commands.CreateTransaction;

namespace PaymentGateway.Api.Controllers;

[ApiController]
[Route("transactions")]
public class TransactionsController : ControllerBase
{
    private readonly IMediator _mediator;

    public TransactionsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("create")]
    public async Task<ActionResult<CreateTransactionResponse>> CreateTransaction(CreateTransactionDto createTransactionDto)
    {
        var request = new CreateTransactionRequest
        {
            CardDetails = createTransactionDto.CardDetails,
            MerchantId = createTransactionDto.MerchantId,
            PaymentAmount = createTransactionDto.PaymentAmount
        };
        var response = await _mediator.Send(request);
        return response;
    }
}