using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using CreditProposal.Domain.Repositories;
using CreditProposal.Domain.Messaging;
using CreditProposal.Infrastructure.Persistence;
using CreditProposal.Infrastructure.Repositories;
using CreditProposal.Infrastructure.Messaging;
using RabbitMQ.Client;

namespace CreditProposal.Infrastructure;

public static class InfrastructureModule
{
    public static IServiceCollection AddInfrastructureModule(this IServiceCollection services)
    {            
        services
            .AddSqlServer()
            .AddRepositories()
            .AddMessageBus();

        return services;
    }

    private static IServiceCollection AddSqlServer(this IServiceCollection services)
    {
        var configuration = services.BuildServiceProvider().GetService<IConfiguration>();
        
        services.AddDbContext<CreditProposalContext>(opt => {
            opt.UseSqlServer(configuration!.GetConnectionString("Default"));
        });

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICreditProposalRepository, CreditProposalRepository>();

        return services;
    }

    private static IServiceCollection AddMessageBus(this IServiceCollection services) 
    {
        // Configurar RabbitMQ Connection e Channel
        services.AddSingleton<IConnection>(sp =>
        {
            var configuration = sp.GetRequiredService<IConfiguration>();
            var rabbitConfig = configuration.GetSection("RabbitMQ").Get<MessageBusConnectionConfigModel>();
            
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration), "RabbitMQ configuration cannot be null.");
            }

            var factory = new ConnectionFactory()
            {
                HostName = rabbitConfig.HostName,
                Port = rabbitConfig.Port,
                UserName = rabbitConfig.UserName,
                Password = rabbitConfig.Password
            };

            return factory.CreateConnection();
        });

        services.AddSingleton<IModel>(sp =>
        {
            var connection = sp.GetRequiredService<IConnection>();
            return connection.CreateModel();
        });

        // Registrar RabbitMqPublisher para publicar mensagens
        services.AddSingleton<IMessageBusPublisher, RabbitMqPublisher>();

        // Registrar RabbitMqSetupService para configurar filas e exchanges
        services.AddSingleton<IMessageBusSetupServer, RabbitMqSetupService>();

        return services;
    }
    
    public static void UseMessageBusSetup(this IServiceProvider services)
    {
        using var scope = services.CreateScope();
        Console.WriteLine("Setup MessageBus...");
        var context = scope.ServiceProvider.GetRequiredService<IMessageBusSetupServer>();
        context.Setup();
        Console.WriteLine("Setup Ok...");
    }
}
