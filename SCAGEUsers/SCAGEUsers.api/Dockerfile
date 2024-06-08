#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["SCAGEUsers/SCAGEUsers.api/SCAGEUsers.api.csproj", "SCAGEUsers.api/"]
COPY ["SCAGEUsers/SCAGEUsers.Application/SCAGEUsers.Application.csproj", "SCAGEUsers/SCAGEUsers.Application/"]
COPY ["SCAGEUsers.Infrastructure/SCAGEUsers.Infrastructure.csproj", "SCAGEUsers.Infrastructure/"]
# COPY ["SCAGEUsers/SCAGEUsers.Infrastructure/SCAGEUsers.Infrastructure.csproj", "SCAGEUsers/SCAGEUsers.Infrastructure/"]
RUN dotnet restore "tiagolopesdocker/scage-users:tagnameSCAGEUsers.api/SCAGEUsers.api.csproj"
COPY . .
WORKDIR "/src/SCAGEUsers/SCAGEUsers.api"
RUN dotnet build "SCAGEUsers.api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SCAGEUsers.api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SCAGEUsers.api.dll"]
