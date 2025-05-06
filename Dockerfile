# Estágio de build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copia os arquivos do projeto
COPY ["src/FIAP.HealthMed.API/FIAP.HealthMed.API.csproj", "src/FIAP.HealthMed.API/"]
COPY ["src/FIAP.HealthMed.Application/FIAP.HealthMed.Application.csproj", "src/FIAP.HealthMed.Application/"]
COPY ["src/FIAP.HealthMed.Domain/FIAP.HealthMed.Domain.csproj", "src/FIAP.HealthMed.Domain/"]
COPY ["src/FIAP.HealthMed.Infrastructure/FIAP.HealthMed.Infrastructure.csproj", "src/FIAP.HealthMed.Infrastructure/"]

# Restaura os pacotes
RUN dotnet restore "src/FIAP.HealthMed.API/FIAP.HealthMed.API.csproj"

# Copia o resto do código
COPY . .

# Publica a aplicação
RUN dotnet publish "src/FIAP.HealthMed.API/FIAP.HealthMed.API.csproj" -c Release -o /app/publish

# Estágio final
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 80
EXPOSE 443
ENTRYPOINT ["dotnet", "FIAP.HealthMed.API.dll"] 