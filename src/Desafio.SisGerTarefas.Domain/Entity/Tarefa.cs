using Desafio.SisGerTarefas.Domain.SeedWork;
using Desafio.SisGerTarefas.Domain.Validation;

namespace Desafio.SisGerTarefas.Domain.Entity
{
    public class Tarefa : AggregateRoot
    {
        public string Titulo { get; private set; }
        public string Descricao { get; private set; }
        public DateTime DataVencimento { get; private set; }
        public Status Status { get; private set; }
        
        public Tarefa(string titulo, string descricao) 
        {
            Titulo = titulo;
            Descricao = descricao;
            DataVencimento = DateTime.Now;
            Status = Status.Pendente;

            Validate();
        }

        public void Update(string titulo, string? descricao = null, DateTime? data = null, Status? status = null)
        {
            Titulo = titulo;
            Descricao = descricao ?? Descricao;
            DataVencimento = data ?? DataVencimento;
            Status = status ?? Status;

            Validate();
        }

        private void Validate()
        {
            DomainValidation.NotNullOrEmpty(Titulo, nameof(Titulo));
            DomainValidation.MinLength(Titulo, 3, nameof(Titulo));
            DomainValidation.MaxLength(Titulo, 255, nameof(Titulo));

            DomainValidation.NotNull(Descricao, nameof(Descricao));
            DomainValidation.MaxLength(Descricao, 10_000, nameof(Descricao));
        }
    }
}
