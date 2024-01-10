FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY WalletApi.sln ./

COPY Wallet.Web/*.csproj ./Wallet.Web/
COPY Wallet.Application/*.csproj ./Wallet.Application/
COPY Wallet.Infrastructure/*.csproj ./Wallet.Infrastructure/
COPY Wallet.Core/*.csproj ./Wallet.Core/
COPY Wallet.Shared/*.csproj ./Wallet.Shared/

RUN dotnet restore
COPY . .
WORKDIR /src/Wallet.Application
RUN dotnet build -c Release -o /app
WORKDIR /src/Wallet.Shared
RUN dotnet build -c Release -o /app
WORKDIR /src/Wallet.Core
RUN dotnet build -c Release -o /app
WORKDIR /src/Wallet.Infrastructure
RUN dotnet build -c Release -o /app

WORKDIR /src/Wallet.Web
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Wallet.Web.dll"]