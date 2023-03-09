using Domain.Entities;

namespace Contracts;

public interface ITarefaRepository
{
    Task<ICollection<TarefaEntity>> ObterTarefas();
    Task<TarefaEntity> ObterTarefaPorCodigo(int codigo);
    Task<RetornoApi> Salvar(TarefaDTO dto);
}
