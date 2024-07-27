using Desafio.SisGerTarefas.Application.Interfaces;
using Desafio.SisGerTarefas.Application.UseCases.Tarefa.CreateTarefa;
using Desafio.SisGerTarefas.Domain.Repository;
using Desafio.SisGerTarefas.Infra.Data.EF;
using Desafio.SisGerTarefas.Infra.Data.EF.Repositories;

namespace Desafio.SisGerTarefas.Api.Configurations
{
    public static class UseCaseConfiguration
    {
        public static IServiceCollection AddUseCases(
            this IServiceCollection services
        )
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(CreateTarefa)));
            services.AddRepositories();
            return services;
        }

        private static IServiceCollection AddRepositories(
            this IServiceCollection services
        )
        {
            services.AddTransient<ITarefaRepository, TarefaRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
