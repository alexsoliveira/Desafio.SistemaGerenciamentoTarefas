using DomainEntity = Desafio.SisGerTarefas.Domain.Entity;

namespace Desafio.SisGerTarefas.Application.UseCases.Tarefa.Common
{
    public class TarefaModelOutput
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime DataVencimento { get; set; }
        public DomainEntity.Status Status { get; set; }

        public TarefaModelOutput(Guid id,
            string titulo,
            string descricao,
            DateTime dataVencimento,
            DomainEntity.Status status)
        {
            Id = id;
            Titulo = titulo;
            Descricao = descricao;
            DataVencimento = dataVencimento;
            Status = status;
        }

        public static TarefaModelOutput FromTarefa(DomainEntity.Tarefa tarefa)
            => new(
                tarefa.Id,
                tarefa.Titulo,
                tarefa.Descricao,
                tarefa.DataVencimento,
                tarefa.Status
            );
    }
}
