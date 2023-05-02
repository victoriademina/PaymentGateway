# syntax=docker/dockerfile:1

FROM mcr.microsoft.com/dotnet/sdk:7.0 as build-env

WORKDIR /src

COPY src .

RUN dotnet publish PaymentGateway.Api/PaymentGateway.Api.csproj -c Release -o /publish

FROM mcr.microsoft.com/dotnet/aspnet:7.0 as runtime

WORKDIR /publish

COPY --from=build-env /publish .

EXPOSE 80

ENTRYPOINT ["dotnet", "PaymentGateway.Api.dll"]
