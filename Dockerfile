
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build-env
WORKDIR /app

COPY . ./

RUN dotnet publish "src/FIAP.HealthMed.API/FIAP.HealthMed.API.csproj" -c Release -o /app/out/api
RUN dotnet publish "src/FIAP_HealthMed.Consumer/FIAP_HealthMed.Consumer.csproj" -c Release -o /app/out/worker


FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS api
WORKDIR /app
COPY --from=build-env /app/out/api ./
ENV ASPNETCORE_ENVIRONMENT=Development
EXPOSE 8080
EXPOSE 443
ENTRYPOINT ["dotnet", "FIAP.HealthMed.API.dll"]


FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS worker
WORKDIR /app
COPY --from=build-env /app/out/worker ./
ENV DOTNET_ENVIRONMENT=Development
ENTRYPOINT ["dotnet", "FIAP_HealthMed.Consumer.dll"]
