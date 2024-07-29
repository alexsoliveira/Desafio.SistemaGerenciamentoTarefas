using Bogus;
using Desafio.SisGerTarefas.Infra.Data.EF.Context;
using Microsoft.EntityFrameworkCore;
using Desafio.SisGerTarefas.Api;
using Microsoft.Extensions.Configuration;

namespace Desafio.SisGerTarefas.EndTEndTests.Base
{
    public class BaseFixture : IDisposable
    {
        protected Faker Faker { get; set; }
        public CustomWebApplicationFactory<Program> WebAppFactory { get; set; }
        public HttpClient HttpClient { get; set; }
        public ApiClient ApiClient { get; set; }
        private readonly string _dbConnectionString;

        public BaseFixture()
        {
            Faker = new Faker("pt_BR");
            WebAppFactory = new CustomWebApplicationFactory<Program>();
            HttpClient = WebAppFactory.CreateClient();

            ApiClient = new ApiClient(HttpClient);
            var configuration = WebAppFactory.Services
                .GetService(typeof(IConfiguration));
            ArgumentNullException.ThrowIfNull(configuration);
            _dbConnectionString = ((IConfiguration)configuration)
                .GetConnectionString("DefaultConnection")!;
        }

        public TarefaDbContext CreateDbContext()
        {
            //var context = new TarefaDbContext(
            //    new DbContextOptionsBuilder<TarefaDbContext>()
            //    .UseInMemoryDatabase("end2end-tests-db")
            //    .Options
            //);
            var context = new TarefaDbContext(
                new DbContextOptionsBuilder<TarefaDbContext>()
                .UseSqlServer(_dbConnectionString)
                .Options                
            );
            return context;
        }

        public void Dispose()
        {
            WebAppFactory.Dispose();
        }
    }
}
