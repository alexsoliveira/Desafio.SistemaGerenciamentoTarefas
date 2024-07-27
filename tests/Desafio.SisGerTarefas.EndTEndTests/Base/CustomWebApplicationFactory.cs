using Desafio.SisGerTarefas.Infra.Data.EF.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Desafio.SisGerTarefas.EndTEndTests.Base
{
    public class CustomWebApplicationFactory<TStartup>
        : WebApplicationFactory<TStartup>
        where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            //builder.ConfigureServices(services => {
            //    var dbOptions = services.FirstOrDefault(
            //        x => x.ServiceType == typeof(
            //            DbContextOptions<TarefaDbContext>
            //        )
            //    );
            //    if (dbOptions is not null)
            //        services.Remove(dbOptions);
            //    services.AddDbContext<TarefaDbContext>(
            //        options => {
            //            options.UseInMemoryDatabase("end2end-tests-db");
            //        }
            //    );
            //});
           
            builder.UseEnvironment("EndToEndTest");
            builder.ConfigureServices(services => {                               
                var serviceProvider = services.BuildServiceProvider();
                using var scope = serviceProvider.CreateScope();                
                var context = scope.ServiceProvider
                    .GetService<TarefaDbContext>();
                ArgumentNullException.ThrowIfNull(context);
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            });            

            base.ConfigureWebHost(builder);
        }
    }
}
