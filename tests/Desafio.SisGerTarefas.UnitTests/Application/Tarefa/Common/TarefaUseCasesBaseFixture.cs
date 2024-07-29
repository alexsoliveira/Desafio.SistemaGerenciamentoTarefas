using Desafio.SisGerTarefas.Application.Interfaces;
using DomainEntity = Desafio.SisGerTarefas.Domain.Entity;
using Desafio.SisGerTarefas.Domain.Repository;
using Desafio.SisGerTarefas.UnitTests.Common;
using Moq;

namespace Desafio.SisGerTarefas.UnitTests.Application.Tarefa.Common
{
    public class TarefaUseCasesBaseFixture 
        : BaseFixture
    {
        public Mock<ITarefaRepository> GetRepositoryMock()
            => new();

        public Mock<IUnitOfWork> GetUnitOfWorkMock()
            => new();

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

        public string GetValidTarefaDescricao()
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
                GetValidTarefaIdUsuario(),
                GetValidTarefaTitulo(),
                GetValidTarefaDescricao()
            );
    }
}
