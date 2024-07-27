using Desafio.SisGerTarefas.Application.Interfaces;
using Desafio.SisGerTarefas.Application.UseCases.Tarefa.Common;
using Desafio.SisGerTarefas.Domain.Repository;
using DomainEntity = Desafio.SisGerTarefas.Domain.Entity;

namespace Desafio.SisGerTarefas.Application.UseCases.Tarefa.CreateTarefa
{
    public class CreateTarefa : ICreateTarefa
    {
        public readonly ITarefaRepository _tarefaRepository;
        public readonly IUnitOfWork _unitOfWork;

        public CreateTarefa(
            ITarefaRepository tarefaRepository, 
            IUnitOfWork unitOfWork)
        {
            _tarefaRepository = tarefaRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<TarefaModelOutput> Handle(
            CreateTarefaInput input,
            CancellationToken cancellationToken
        ) {
            var tarefa = new DomainEntity.Tarefa(input.Titulo, input.Descricao);

            await _tarefaRepository.Insert(tarefa,cancellationToken);
            await _unitOfWork.Commit(cancellationToken);

            return TarefaModelOutput.FromTarefa(tarefa);
        }
    }
}
