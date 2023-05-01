using MediatR;
using Microsoft.AspNetCore.Mvc;
using PaymentGateway.Api.Dtos;
using PaymentGateway.Application.Commands.CreateTransaction;
using PaymentGateway.Application.Queries.GetTransaction;
using Serilog;

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

    /// <summary>
    /// Retrieves a transaction by merchant ID and transaction ID.
    /// </summary>
    /// <returns>The response containing the new transaction ID and its status.</returns>
    [HttpGet("{merchantId}/{transactionId}")]
    public async Task<ActionResult<GetTransactionResponse>> GetTransaction(Guid merchantId, Guid transactionId)
    {
        Log.Information(
            "Received a request to retrieve a transaction with Merchant ID: {MerchantId} and Transaction ID: {TransactionID}", 
            merchantId, transactionId);
        var request = new GetTransactionRequest
        {
            MerchantId = merchantId,
            TransactionId = transactionId
        };
        var response = await _mediator.Send(request);
        Log.Information("Retrieved a transaction with Transaction ID: {TransactionId}", response.TransactionId);
        return response;
    }
    
    /// <summary>
    /// Creates a new transaction.
    /// </summary>
    /// <returns>The response containing the new transaction ID and its status.</returns>
    [HttpPost("create")]
    public async Task<ActionResult<CreateTransactionResponse>> CreateTransaction(CreateTransactionDto createTransactionDto)
    {
        Log.Information("Received a request to create a new transaction");
        var request = new CreateTransactionRequest
        {
            CardDetails = createTransactionDto.CardDetails,
            MerchantId = createTransactionDto.MerchantId,
            PaymentAmount = createTransactionDto.PaymentAmount
        };
        var response = await _mediator.Send(request);
        Log.Information("Created a transaction with ID: {Id}", response.TransactionId);
        return response;
    }
}