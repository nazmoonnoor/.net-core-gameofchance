#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/GameOfChance.Api/GameOfChance.Api.csproj", "src/GameOfChance.Api/"]
COPY ["src/GameOfChance.Client/GameOfChance.Client.csproj", "src/GameOfChance.Client/"]
COPY ["src/GameOfChance.Core/GameOfChance.Core.csproj", "src/GameOfChance.Core/"]
COPY ["src/Core/GameOfChance.SharedKernel/GameOfChance.SharedKernel.csproj", "src/Core/GameOfChance.SharedKernel/"]
COPY ["src/GameOfChance.Infrastructure/GameOfChance.Infrastructure.csproj", "src/GameOfChance.Infrastructure/"]
RUN dotnet restore "src/GameOfChance.Api/GameOfChance.Api.csproj"
COPY . .
WORKDIR "/src/src/GameOfChance.Api"
RUN dotnet build "GameOfChance.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GameOfChance.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GameOfChance.Api.dll"]