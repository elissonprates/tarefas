using Domain.Entities;

namespace Contracts;

public interface IRabbitMQSender
{
    void SendMessage(TarefaEntity tarefa);
}
