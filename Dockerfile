# Estágio de build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build-env
WORKDIR /app
# Copia os arquivos do projeto
COPY . ./
# Publica a aplicação
RUN dotnet publish "src/FIAP.HealthMed.API/FIAP.HealthMed.API.csproj" -c Release -o /app/out/api

# Estágio final
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS api
WORKDIR /app
COPY --from=build-env /app/out/api ./
ENV ASPNETCORE_ENVIRONMENT="Development"
EXPOSE 8080
EXPOSE 443
ENTRYPOINT ["dotnet", "FIAP.HealthMed.API.dll"] 