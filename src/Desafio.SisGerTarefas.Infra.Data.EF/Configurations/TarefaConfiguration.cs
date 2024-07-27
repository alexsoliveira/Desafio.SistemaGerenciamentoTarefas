using Desafio.SisGerTarefas.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Desafio.SisGerTarefas.Infra.Data.EF.Configurations
{
    internal class TarefaConfiguration
        : IEntityTypeConfiguration<Tarefa>
    {
        public void Configure(EntityTypeBuilder<Tarefa> builder)
        {
            builder.HasKey(tarefa => tarefa.Id);
            builder.Property(tarefa => tarefa.Titulo)
                .HasMaxLength(255);
            builder.Property(tarefa => tarefa.Descricao)
                .HasMaxLength(10_000);
        }
    }
}
