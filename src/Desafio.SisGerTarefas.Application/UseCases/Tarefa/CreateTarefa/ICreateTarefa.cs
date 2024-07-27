using Desafio.SisGerTarefas.Application.UseCases.Tarefa.Common;
using MediatR;

namespace Desafio.SisGerTarefas.Application.UseCases.Tarefa.CreateTarefa
{
    public interface ICreateTarefa
        :IRequestHandler<CreateTarefaInput, TarefaModelOutput>
    { }
}
