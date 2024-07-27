using Bogus;
using Desafio.SisGerTarefas.Infra.Data.EF.Context;
using Microsoft.EntityFrameworkCore;

namespace Desafio.SisGerTarefas.IntegrationTests.Base
{
    public class BaseFixture
    {
        protected Faker Faker { get; set; }

        public BaseFixture()
            => Faker = new Faker("pt_BR");

        public TarefaDbContext CreateDbContext(bool preserveData = false)
        {
            var context = new TarefaDbContext(
                new DbContextOptionsBuilder<TarefaDbContext>()
                .UseInMemoryDatabase("integration-tests-db")
                .Options
            );
            if (preserveData == false)
                context.Database.EnsureDeleted();
            return context;
        }
    }
}
