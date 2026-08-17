# Build stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["Round-OP.csproj", "./"]
RUN dotnet restore "Round-OP.csproj"

COPY . .
RUN dotnet publish "Round-OP.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:${PORT}

ENTRYPOINT ["dotnet", "Round-OP.dll"]