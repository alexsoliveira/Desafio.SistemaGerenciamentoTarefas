using Desafio.SisGerTarefas.Domain.Entity;
using Desafio.SisGerTarefas.IntegrationTests.Base;

namespace Desafio.SisGerTarefas.IntegrationTests.Infra.Data.EF.UnitOfWork
{
    [CollectionDefinition(nameof(UnitOfWorkTestFixture))]
    public class UnitOfWorkTestFixtureColection
        : ICollectionFixture<UnitOfWorkTestFixture>
    { }

    public class UnitOfWorkTestFixture
        : BaseFixture
    {
        public string GetValidTarefaIdUsuario()
        {
            var tarefaIdUsuario = Guid.NewGuid();

            return tarefaIdUsuario.ToString();
        }

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
            GetValidTarefaIdUsuario(),
            GetValidTarefaTitulo(),
            GetValidTarefaDescription()
        );

        public List<Tarefa> GetExampleTarefaList(int length = 10)
            => Enumerable.Range(1, length)
            .Select(_ => GetExampleTarefa()).ToList();
    }
}
