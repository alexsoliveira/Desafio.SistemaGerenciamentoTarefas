using Desafio.SisGerTarefas.Infra.Data.EF.Context;
using Desafio.WebAPI.Core.Services.Identidade;
using Microsoft.EntityFrameworkCore;

namespace Desafio.SisGerTarefas.Api.Configurations
{
    public static class ConnectionsConfiguration
    {
        public static IServiceCollection AddAppConnections(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            services.AddDbConnection(configuration);
            return services;
        }

        private static IServiceCollection AddDbConnection(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            //services.AddDbContext<TarefaDbContext>(
            //    options => options.UseInMemoryDatabase(
            //        "InMemory-DSV-Database"
            //    )
            //);
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<TarefaDbContext>(
                options => options.UseSqlServer(
                    connectionString
                )
            );

            services.AddJwtConfiguration(configuration);

            return services;
        }
    }
}
