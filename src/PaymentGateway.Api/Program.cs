using BankSimulator.Sdk;
using PaymentGateway.Application;
using PaymentGateway.Application.Common.Bank;
using PaymentGateway.Application.Common.Repository;
using PaymentGateway.Infrastructure.BankSimulatorAdapter;
using PaymentGateway.Infrastructure.Persistence;
using Serilog;


Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureApplication();
builder.Services.AddSingleton<UltimateBankClient>();
builder.Services.AddSingleton<IBankAdapter, UltimateBankClientAdapter>();
builder.Services.AddSingleton<PaymentGatewayDbContext>();
builder.Services.AddSingleton<IPaymentGatewayRepository, PaymentGatewayRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();