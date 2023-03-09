using Contracts;
using Microsoft.Extensions.Logging;

namespace Application;

public class TarefaService : ITarefaService
{
    private readonly ILogger _logger;
    private readonly ITarefaRepository _repository;
    public TarefaService(ITarefaRepository repository, ILogger<TarefaService> logger)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<RetornoApi<TarefaDTO>> ObterTarefaPorCodigo(int codigo)
    {
        var tarefa = await _repository.ObterTarefaPorCodigo(codigo);
        throw new NotImplementedException();
    }

    public async Task<RetornoApi<ICollection<TarefaDTO>>>  ObterTarefas()
    {
        var tarefas = await _repository.ObterTarefas();
        throw new NotImplementedException();
    }

    public async Task<RetornoApi> Salvar(TarefaDTO dto)
    {
        throw new NotImplementedException();
    }
}