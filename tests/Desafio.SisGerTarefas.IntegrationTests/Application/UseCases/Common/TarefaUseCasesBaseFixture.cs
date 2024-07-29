using Desafio.SisGerTarefas.IntegrationTests.Base;
using DomainEntity = Desafio.SisGerTarefas.Domain.Entity;

namespace Desafio.SisGerTarefas.IntegrationTests.Application.UseCases.Common
{
    public class TarefaUseCasesBaseFixture
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

        public DomainEntity.Tarefa GetExampleTarefa()
        => new(
            GetValidTarefaIdUsuario(),
            GetValidTarefaTitulo(),
            GetValidTarefaDescription()
        );
    }
}
