#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 5002
EXPOSE 5003

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["./Desafio.SisGerTarefas.Api/Desafio.SisGerTarefas.Api.csproj", "src/Desafio.SisGerTarefas.Api/"]
RUN dotnet restore "src/Desafio.SisGerTarefas.Api/Desafio.SisGerTarefas.Api.csproj"
COPY . .
WORKDIR "/src/Desafio.SisGerTarefas.Api"
RUN dotnet build "./Desafio.SisGerTarefas.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Desafio.SisGerTarefas.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Desafio.SisGerTarefas.Api.dll"]