using Desafio.SisGerTarefas.UnitTests.Common;
using DomainEntity = Desafio.SisGerTarefas.Domain.Entity;

namespace Desafio.SisGerTarefas.UnitTests.Domain.Entity.Tarefa
{
    [CollectionDefinition(nameof(TarefaTestFixture))]
    public class TarefaTestFixtureCollection
    : ICollectionFixture<TarefaTestFixture>
    { }

    public class TarefaTestFixture : BaseFixture
    {
        public TarefaTestFixture()
        : base() { }

        public string GetValidTarefaTitulo()
        {
            var tarefaName = "";

            while (tarefaName.Length < 3)
                tarefaName = Faker.Commerce.Categories(1)[0];

            if (tarefaName.Length > 255)
                tarefaName = tarefaName[..255];

            return tarefaName;
        }

        public string GetValidTarefaDescription()
        {
            var tarefaDescription =
                Faker.Commerce.ProductDescription();

            if (tarefaDescription.Length > 10_000)
                tarefaDescription
                    = tarefaDescription[..10_000];

            return tarefaDescription;
        }

        public DomainEntity.Tarefa GetValidTarefa()
        => new(
            GetValidTarefaTitulo(),
            GetValidTarefaDescription()
        );
    }
}
