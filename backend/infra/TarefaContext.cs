using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infra;

public class TarefaContext: DbContext
{
    public DbSet<TarefaEntity> Tarefa { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<TarefaEntity>(x =>
        {
            x.ToTable("tb_tarefas", _ => _.IsTemporal(
                 ttb =>
                 {
                     ttb.HasPeriodStart("data_inicio_vigencia");
                     ttb.HasPeriodEnd("data_fim_vigencia");
                     ttb.UseHistoryTable("tb_tarefas_historico");
                 }
            ));
            x.HasKey(x => x.Codigo);
        });
    }

    public TarefaContext()
    {

    }
    public TarefaContext(DbContextOptions<TarefaContext> options) : base(options)
    {

    }

}