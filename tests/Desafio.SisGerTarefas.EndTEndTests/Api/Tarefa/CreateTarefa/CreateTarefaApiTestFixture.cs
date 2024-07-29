using Desafio.SisGerTarefas.Application.UseCases.Tarefa.CreateTarefa;
using Desafio.SisGerTarefas.EndTEndTests.Api.Tarefa.Common;

namespace Desafio.SisGerTarefas.EndTEndTests.Api.Tarefa.CreateTarefa
{
    [CollectionDefinition(nameof(CreateTarefaApiTestFixture))]
    public class CreateTarefaApiTestFixtureCollection
    : ICollectionFixture<CreateTarefaApiTestFixture>
    { }

    public class CreateTarefaApiTestFixture
        : TarefaBaseFixture
    {
        public CreateTarefaInput GetExampleInput()
            => new(
                GetValidTarefaIdUsuario(),
                GetValidTarefaTitulo(),
                GetValidTarefaDescription()                
            );
    }
}
