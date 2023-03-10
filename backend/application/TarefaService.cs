using Microsoft.Extensions.Logging;
using Domain.Entities;
using AutoMapper;
using Contracts;

namespace Application;

public class TarefaService : ITarefaService
{
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    private readonly IRabbitMQSender _sender;
    private readonly ITarefaRepository _repository;
    public TarefaService(IMapper mapper, ITarefaRepository repository, IRabbitMQSender sender, ILogger<TarefaService> logger)
    {
        _mapper = mapper;
        _logger = logger;
        _repository = repository;
        _sender = sender;
    }

    public async Task<RetornoApi<TarefaDTO>> ObterTarefaPorCodigo(int? codigo)
    {
        var tarefa = await _repository.ObterTarefaPorCodigo(codigo);
        if (tarefa == null)
        {
            return new RetornoApi<TarefaDTO>("Tarefa não encontrada", false);
        }

        var dto = _mapper.Map<TarefaDTO>(tarefa);
        return new RetornoApi<TarefaDTO>(dto);
    }

    public async Task<RetornoApi<TarefaDTO>> ObterTarefas()
    {
        var tarefas = await _repository.ObterTarefas();
        var dtos = _mapper.Map<ICollection<TarefaDTO>>(tarefas);
        return new RetornoApi<TarefaDTO>(dtos);
    }

    public async Task<RetornoApi> Incluir(TarefaDTO dto)
    {
        if (dto == null)
            return new RetornoApi("Tarefa inválida", false);

        var tarefa = _mapper.Map<TarefaEntity>(dto);

        tarefa.Codigo = null;

        (var sucesso, var erros) = tarefa.Validar();

        if (!sucesso)
            return new RetornoApi(erros, sucesso);

        await _repository.Incluir(tarefa);

        return new RetornoApi();
    }

    public async Task<RetornoApi> Alterar(TarefaDTO dto)
    {
        if (dto == null)
            return new RetornoApi("Tarefa inválida", false);

        var tarefa = _mapper.Map<TarefaEntity>(dto);

        (var sucesso, var erros) = tarefa.Validar();

        if (!sucesso)
            return new RetornoApi(erros, sucesso);

        var existeTarefa = await ObterTarefaPorCodigo(tarefa.Codigo);

        if (existeTarefa == null)
            return new RetornoApi("Tarefa não existe", false);

        await _repository.Alterar(tarefa);

        return new RetornoApi();
    }

    public async Task<RetornoApi> Excluir(int? codigo)
    {
        var existeTarefa = await _repository.ObterTarefaPorCodigo(codigo);

        if (existeTarefa == null)
            return new RetornoApi("Tarefa não existe", false);

        await _repository.Excluir(codigo);

        _sender.SendMessage(existeTarefa);

        return new RetornoApi();
    }
}