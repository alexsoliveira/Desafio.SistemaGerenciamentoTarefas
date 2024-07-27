using Desafio.SisGerTarefas.Application.UseCases.Tarefa.CreateTarefa;
using Desafio.SisGerTarefas.IntegrationTests.Application.UseCases.Common;

namespace Desafio.SisGerTarefas.IntegrationTests.Application.UseCases.CreateTarefa
{
    [CollectionDefinition(nameof(CreateTarefaTestFixture))]
    public class CreateTarefaTestFixtureCollection
        : ICollectionFixture<CreateTarefaTestFixture>
    { }

    public class CreateTarefaTestFixture
        : TarefaUseCasesBaseFixture
    {
        public CreateTarefaInput GetInput()
        {
            var tarefa = GetExampleTarefa();

            return new CreateTarefaInput(
                tarefa.Titulo,
                tarefa.Descricao
            );
        }

        public CreateTarefaInput GetInvalidInputShortTitulo()
        {
            var invalidInputShortTitulo = GetInput();
            invalidInputShortTitulo.Titulo = invalidInputShortTitulo.Titulo.Substring(0, 2);
            return invalidInputShortTitulo;
        }

        public CreateTarefaInput GetInvalidInputTooLongTitulo()
        {
            var invalidInputTooLongTitulo = GetInput();
            var tooLongTituloForTarefa = Faker.Commerce.ProductName();
            while (tooLongTituloForTarefa.Length <= 255)
                tooLongTituloForTarefa = $"{tooLongTituloForTarefa} {Faker.Commerce.ProductName()}";
            invalidInputTooLongTitulo.Titulo = tooLongTituloForTarefa;
            return invalidInputTooLongTitulo;
        }

        public CreateTarefaInput GetInvalidInputDescriptionNull()
        {
            var invalidInputDescriptionNull = GetInput();
            invalidInputDescriptionNull.Descricao = null!;
            return invalidInputDescriptionNull;
        }

        public CreateTarefaInput GetInvalidInputTooLongDescription()
        {
            var invalidInputTooLongDescription = GetInput();
            var tooLongDescriptionForCategory = Faker.Commerce.ProductDescription();
            while (tooLongDescriptionForCategory.Length <= 10_000)
                tooLongDescriptionForCategory = $"{tooLongDescriptionForCategory} {Faker.Commerce.ProductDescription()}";
            invalidInputTooLongDescription.Descricao = tooLongDescriptionForCategory;
            return invalidInputTooLongDescription;
        }
    }
}
