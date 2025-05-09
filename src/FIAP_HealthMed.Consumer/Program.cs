

using System.Data;
using FIAP_HealthMed.Consumer;
using FIAP_HealthMed.Consumer.Consumer;
using FIAP_HealthMed.Consumer.Interface;
using FIAP_HealthMed.Consumer.Service;
using FIAP_HealthMed.CrossCutting;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MySqlConnector;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        var configuration = hostContext.Configuration;

        // Obtém configurações do launchSettings.json
        var fila = Environment.GetEnvironmentVariable("MassTransit_Filas_UsuarioFila") ?? string.Empty;
        var servidor = Environment.GetEnvironmentVariable("MassTransit_Servidor") ?? string.Empty;
        var usuario = Environment.GetEnvironmentVariable("MassTransit_Usuario") ?? string.Empty;
        var senha = Environment.GetEnvironmentVariable("MassTransit_Senha") ?? string.Empty;
        var connectionString = Environment.GetEnvironmentVariable("Connection_String");

        // Registra serviços 
        services.AddScoped<IUsuarioConsumerService, UsuarioConsumerService>();
        services.AddRegisterCommonServices();

        //Configuração para buscar a connection        
        services.AddScoped<IDbConnection>((connection) => new MySqlConnection(connectionString));

        // Configura o Worker como serviço hospedado
        services.AddHostedService<Worker>();

        // Configura MassTransit
        services.AddMassTransit(x =>
        {
            // Registra o consumidor específico
            x.AddConsumer<UsuarioConsumer>();

            // Configura o RabbitMQ
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(servidor, "/", h =>
                {
                    h.Username(usuario);
                    h.Password(senha);
                });

                // Configura o endpoint para consumir mensagens da fila
                cfg.ReceiveEndpoint(fila + "-Cadastrar", e =>
                {
                    e.ConfigureConsumer<UsuarioConsumer>(context);
                });
                cfg.ReceiveEndpoint(fila + "-Atualizar", e =>
                {
                    e.ConfigureConsumer<UsuarioConsumer>(context);
                });
                cfg.ReceiveEndpoint(fila + "-Deletar", e =>
                {
                    e.ConfigureConsumer<UsuarioConsumer>(context);
                });

                cfg.ConfigureEndpoints(context);
            });
        });
    })
    .Build();

// Executa o host
host.Run();