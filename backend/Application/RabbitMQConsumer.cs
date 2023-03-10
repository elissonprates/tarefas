using Contracts;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace Application;

public class RabbitMQConsumer : BackgroundService
{
    private IConnection _connection;

    private readonly ILogger _logger;

    private readonly string _hostName;
    private readonly string _password;
    private readonly string _userName;
    private readonly IConfiguration _config;

    public RabbitMQConsumer(ILogger<RabbitMQConsumer> logger, IConfiguration config)
    {
        _config = config;
        _logger = logger;
        _hostName = _config.GetValue<string>("RabbitMQ:Local:HostName") ?? string.Empty;
        _password = _config.GetValue<string>("RabbitMQ:Local:Password") ?? string.Empty;
        _userName = _config.GetValue<string>("RabbitMQ:Local:UserName") ?? string.Empty;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var queueName = "TarefasExcluidas";

        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("RabbitMQConsumer --> ExecuteAsync");

            if (ConnectionExists())
            {
                try
                {
                    using var channel = _connection.CreateModel();

                    //channel.QueueDeclare(queue: queueName, durable: true, autoDelete: false);
                    channel.QueueDeclare(queue: queueName, durable: true, autoDelete: false, exclusive: false);

                    stoppingToken.ThrowIfCancellationRequested();
                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += async (c, evt) =>
                    {
                        var content = Encoding.UTF8.GetString(evt.Body.ToArray());
                        var message = JsonSerializer.Deserialize<MessageInQueue<TarefaEntity>>(content);

                        _logger.LogInformation($"Message in queue: {content}");

                    };
                    channel.BasicConsume(queueName, false, consumer);

                    await Task.Delay(-1, stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Erro no método ExecuteAsync");
                    await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
                }
            }
            await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
        }
    }
    private void CreateConnection()
    {
        try
        {
            var factory = new ConnectionFactory()
            {
                HostName = _hostName,
                UserName = _userName,
                Password = _password
            };

            _connection = factory.CreateConnection();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"[RabbitMQConsumer] Erro na tentativa de criar uma conexão com o servidor RabbitMQ {_hostName}");
        }
    }

    private bool ConnectionExists()
    {
        if (_connection != null) return true;
        CreateConnection();
        return _connection != null;
    }
}
