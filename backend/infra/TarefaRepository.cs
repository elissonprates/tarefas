using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Contracts;
using Microsoft.Extensions.Logging;

namespace Infra;

public class TarefaRepository : ITarefaRepository
{
    private readonly ILogger _logger;
    private readonly TarefaContext _context;
    public TarefaRepository(TarefaContext context, ILogger<TarefaRepository> logger)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<TarefaEntity?> ObterTarefaPorCodigo(int? codigo)
    {
        return await _context.Tarefa
            .AsNoTracking()
            .Where(x => x.Codigo == codigo)
            .FirstOrDefaultAsync();
    }

    public async Task<ICollection<TarefaEntity>> ObterTarefas()
    {
        return await _context.Tarefa
           .AsNoTracking()
           .ToListAsync();
    }

    public async Task Incluir(TarefaEntity tarefa)
    {
        await _context.Tarefa.AddAsync(tarefa);
        await _context.SaveChangesAsync();
    }

    public async Task Alterar(TarefaEntity tarefa)
    {
        _context.Tarefa.Attach(tarefa);
        _context.Entry(tarefa).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task Excluir(int? codigo)
    {
        await _context.Tarefa
            .Where(tarefa => tarefa.Codigo == codigo)
            .ExecuteDeleteAsync();

        await _context.SaveChangesAsync();
    }
}
