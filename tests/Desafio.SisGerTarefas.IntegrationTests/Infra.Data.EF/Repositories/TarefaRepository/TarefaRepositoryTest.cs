using Desafio.SisGerTarefas.Infra.Data.EF.Context;
using Repository = Desafio.SisGerTarefas.Infra.Data.EF.Repositories;
using FluentAssertions;

namespace Desafio.SisGerTarefas.IntegrationTests.Infra.Data.EF.Repositories.TarefaRepository
{
    [Collection(nameof(TarefaRepositoryTestFixture))]

    public class TarefaRepositoryTest        
    {
        private readonly TarefaRepositoryTestFixture _fixture;

        public TarefaRepositoryTest(TarefaRepositoryTestFixture fixture)
            => _fixture = fixture;

        [Fact(DisplayName = nameof(Insert))]
        [Trait("Integration/Infra.Data", "CategoryRepository - Repositories")]
        public async Task Insert()
        {
            TarefaDbContext dbContext = _fixture.CreateDbContext();
            var exampleTarefa = _fixture.GetExampleTarefa();
            var categoryRepository = new Repository.TarefaRepository(dbContext);

            await categoryRepository.Insert(exampleTarefa, CancellationToken.None);
            await dbContext.SaveChangesAsync(CancellationToken.None);

            var dbCategory = await (_fixture.CreateDbContext(true))
                .Tarefas.FindAsync(exampleTarefa.Id);
            dbCategory.Should().NotBeNull();
            dbCategory!.Titulo.Should().Be(exampleTarefa.Titulo);
            dbCategory.Descricao.Should().Be(exampleTarefa.Descricao);
            dbCategory.Status.Should().Be(exampleTarefa.Status);
            dbCategory.DataVencimento.Should().Be(exampleTarefa.DataVencimento);
        }
    }
}
