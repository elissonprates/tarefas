using Microsoft.EntityFrameworkCore;

namespace infra;

public class MeuContext: DbContext
{

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Perfil>(perfil =>
        {
            perfil.ToTable("perfil", _ => _.IsTemporal(
                 ttb =>
                 {
                     ttb.HasPeriodStart("data_inicio_vigencia");
                     ttb.HasPeriodEnd("data_fim_vigencia");
                     ttb.UseHistoryTable("perfil_historico");
                 }
            ));

            perfil.HasOne<Usuario>()
                .WithMany()
                .HasForeignKey(x => x.usuario_id);

            perfil.Property(_ => _.data_atualizacao).HasDefaultValueSql("getdate()");

        });
    }
}