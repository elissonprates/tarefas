namespace Contracts;

public interface IRabbitMQSender
{
    void SendMessage(TarefaDTO tarefa);
}
