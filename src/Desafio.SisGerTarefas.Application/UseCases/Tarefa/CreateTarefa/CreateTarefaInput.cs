using Desafio.SisGerTarefas.Application.UseCases.Tarefa.Common;
using MediatR;
using DomainEntity = Desafio.SisGerTarefas.Domain.Entity;

namespace Desafio.SisGerTarefas.Application.UseCases.Tarefa.CreateTarefa
{
    public class CreateTarefaInput : IRequest<TarefaModelOutput>
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime DataVencimento { get; set; }
        public DomainEntity.Status Status { get; set; }

        public CreateTarefaInput(
            string titulo,
            string? descricao = null
            )
        {
            Titulo = titulo;
            Descricao = descricao ?? "";
            DataVencimento = DateTime.Now;
            Status = DomainEntity.Status.Pendente;
        }
    }
}
