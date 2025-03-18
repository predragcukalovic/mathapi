FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
COPY . .
RUN dir -s
RUN dotnet restore Api/Papi.GameServer.Math.NetCore.Api/Papi.GameServer.Math.NetCore.Api.csproj

FROM build AS publish
RUN dotnet publish Api/Papi.GameServer.Math.NetCore.Api/Papi.GameServer.Math.NetCore.Api.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .

ENTRYPOINT ["dotnet", "Papi.GameServer.Math.NetCore.Api.csproj.dll"]