using Infra;
using Microsoft.EntityFrameworkCore;

namespace Api.Config;

public static class DataBaseConfiguration
{
    public static void AddDataBaseConfiguration(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<TarefaContext>(options =>

             options.UseSqlServer(builder.Configuration.GetConnectionString("Local"),
                 sqlServerOptions =>
                 {
                     sqlServerOptions.MigrationsAssembly("api");
                     sqlServerOptions.CommandTimeout(300);
                 }
             ), ServiceLifetime.Scoped, ServiceLifetime.Scoped
        );
    }
}
