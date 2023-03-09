namespace Contracts;

public interface ITarefaService
{
    Task<RetornoApi<ICollection<TarefaDTO>>> ObterTarefas();
    Task<RetornoApi<TarefaDTO>> ObterTarefaPorCodigo(int codigo);
    Task<RetornoApi> Salvar(TarefaDTO dto);
}
