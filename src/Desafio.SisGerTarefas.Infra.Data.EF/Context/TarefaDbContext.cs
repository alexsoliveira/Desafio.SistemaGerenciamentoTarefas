using Desafio.SisGerTarefas.Domain.Entity;
using Desafio.SisGerTarefas.Infra.Data.EF.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Desafio.SisGerTarefas.Infra.Data.EF.Context
{
    public class TarefaDbContext : DbContext
    {
        public DbSet<Tarefa> Tarefas => Set<Tarefa>();

        public TarefaDbContext(
            DbContextOptions<TarefaDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TarefaConfiguration());
        }
    }
}
