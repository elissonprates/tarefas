using Domain.Entities;

namespace Contracts;

public interface ITarefaRepository
{
    Task<ICollection<TarefaEntity>> ObterTarefas();
    Task<TarefaEntity?> ObterTarefaPorCodigo(int? codigo);
    Task Incluir(TarefaEntity tarefa);
    Task Alterar(TarefaEntity tarefa);
    Task Excluir(int? codigo);
}
