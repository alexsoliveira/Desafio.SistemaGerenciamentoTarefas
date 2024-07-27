using Bogus;
using Desafio.SisGerTarefas.Domain.Entity;
using Desafio.SisGerTarefas.IntegrationTests.Base;

namespace Desafio.SisGerTarefas.IntegrationTests.Infra.Data.EF.Repositories.TarefaRepository
{
    [CollectionDefinition(nameof(TarefaRepositoryTestFixture))]
    public class TarefaRepositoryTestCollection
        : ICollectionFixture<TarefaRepositoryTestFixture>
    { }

    public class TarefaRepositoryTestFixture
        : BaseFixture
    {
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

        public Tarefa GetExampleTarefa()
        => new(
            GetValidTarefaTitulo(),
            GetValidTarefaDescription()
        );

        public List<Tarefa> GetExampleTarefaList(int length = 10)
            => Enumerable.Range(1, length)
            .Select(_ => GetExampleTarefa()).ToList();
    }
}
