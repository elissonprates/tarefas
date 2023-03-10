using Contracts;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Application;

public class RabbitMQSender : IRabbitMQSender
{
    private readonly IConfiguration _config;
    private readonly ILogger _logger;

    private readonly string _hostName;
    private readonly string _password;
    private readonly string _userName;

    private IConnection? _connection;

    public RabbitMQSender(IConfiguration config, ILogger<RabbitMQSender> logger)
    {
        _config = config;
        _logger = logger;
        _hostName = _config.GetValue<string>("RabbitMQ:Local:HostName") ?? string.Empty;
        _password = _config.GetValue<string>("RabbitMQ:Local:Password") ?? string.Empty;
        _userName = _config.GetValue<string>("RabbitMQ:Local:UserName") ?? string.Empty;
    }

    public void SendMessage(TarefaEntity tarefa)
    {
        var queueName = $"TarefasExcluidas";

        var message = new MessageInQueue<TarefaEntity>("Tarefa Excluida", tarefa);

        if (ConnectionExists())
        {
            try
            {
                using var channel = _connection.CreateModel();

                //channel.QueueDeclare(queue: queueName, false, false, false, arguments: null);
                channel.QueueDeclare(queue: queueName, durable: true, autoDelete: false, exclusive: false);

                byte[] body = GetMessageAsByteArray(message);

                channel.BasicPublish(exchange: string.Empty, routingKey: queueName, basicProperties: null, body: body);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "SendMessage --> Conexão esta aberta mas deu erro ao enviar a mensagem");
            }
        }
        else
        {
            _logger.LogError("SendMessage --> Conexão não foi aberta");
        }
    }

    private byte[] GetMessageAsByteArray<T>(MessageInQueue<T> message)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
        };
        var json = JsonSerializer.Serialize(message, options);
        var body = Encoding.UTF8.GetBytes(json);
        return body;
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
            _logger.LogError(ex, $"[RabbitMQSender] Erro na tentativa de criar uma conexão com o servidor RabbitMQ {_hostName}");
        }
    }

    private bool ConnectionExists()
    {
        if (_connection != null) return true;
        CreateConnection();
        return _connection != null;
    }
}
