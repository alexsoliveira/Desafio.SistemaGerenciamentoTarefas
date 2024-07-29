#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["./Desafio.Identidade.Api/Desafio.Identidade.Api.csproj", "src/Desafio.Identidade.Api/"]
RUN dotnet restore "src/Desafio.Identidade.Api/Desafio.Identidade.Api.csproj"
COPY . .
WORKDIR "/src/Desafio.Identidade.Api"
RUN dotnet build "./Desafio.Identidade.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Desafio.Identidade.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Desafio.Identidade.Api.dll"]