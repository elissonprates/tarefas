using Domain.Entities;

namespace Contracts;

public interface ITarefaService
{
    Task<RetornoApi<TarefaDTO>> ObterTarefas();
    Task<RetornoApi<TarefaDTO>> ObterTarefaPorCodigo(int? codigo);
    Task<RetornoApi> Incluir(TarefaDTO dto);
    Task<RetornoApi> Alterar(TarefaDTO dto);
    Task<RetornoApi> Excluir(int? codigo);
}
