using Desafio.SisGerTarefas.Application.UseCases.Tarefa.CreateTarefa;
using Desafio.SisGerTarefas.UnitTests.Application.Tarefa.Common;

namespace Desafio.SisGerTarefas.UnitTests.Application.Tarefa.CreateTarefa
{
    [CollectionDefinition(nameof(CreateTarefaTestFixture))]
    public class CreateTarefaTestFixtureCollection
    : ICollectionFixture<CreateTarefaTestFixture>
    { }

    public class CreateTarefaTestFixture : TarefaUseCasesBaseFixture
    {
        public CreateTarefaInput GetInput()
        => new(
            GetValidTarefaIdUsuario(),
            GetValidTarefaTitulo(),
            GetValidTarefaDescricao()
        );

        public CreateTarefaInput GetInvalidInputIdUsuarioGuid()
        {
            var invalidInputIdUsuario = GetInput();
            invalidInputIdUsuario.IdUsuario = Guid.Empty.ToString();
            return invalidInputIdUsuario;
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
            var tooLongDescriptionForTarefa = Faker.Commerce.ProductDescription();
            while (tooLongDescriptionForTarefa.Length <= 10_000)
                tooLongDescriptionForTarefa = $"{tooLongDescriptionForTarefa} {Faker.Commerce.ProductDescription()}";
            invalidInputTooLongDescription.Descricao = tooLongDescriptionForTarefa;
            return invalidInputTooLongDescription;
        }
    }
}
