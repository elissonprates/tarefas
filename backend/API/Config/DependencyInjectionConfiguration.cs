using Application;
using Contracts;
using Infra;

namespace Api.Config;

public static class DependencyInjectionConfiguration
{
    public static void AddDependencyInjection(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ITarefaService, TarefaService>();
        builder.Services.AddScoped<ITarefaRepository, TarefaRepository>();
        builder.Services.AddScoped<IRabbitMQSender, RabbitMQSender>();
    }
}
