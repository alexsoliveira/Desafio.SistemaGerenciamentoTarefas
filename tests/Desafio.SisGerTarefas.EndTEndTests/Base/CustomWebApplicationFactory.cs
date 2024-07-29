using Desafio.Identidade.Api.Data;
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
                var serviceProviderTarefa = services.BuildServiceProvider();             

                using var scopeTarefa = serviceProviderTarefa.CreateScope();
                var contextTarefa = scopeTarefa.ServiceProvider
                    .GetService<TarefaDbContext>();
                ArgumentNullException.ThrowIfNull(contextTarefa);
                
                contextTarefa.Database.EnsureDeleted();                
                contextTarefa.Database.EnsureCreated();


                //var serviceProviderIdentity = services.BuildServiceProvider();
                //var serviceProviderTarefa = services.BuildServiceProvider();

                //using var scopeIdentity = serviceProviderIdentity.CreateScope();
                //var contextIdentity = scopeIdentity.ServiceProvider
                //    .GetService<ApplicationDbContext>();
                //ArgumentNullException.ThrowIfNull(contextIdentity);

                //using var scopeTarefa = serviceProviderTarefa.CreateScope();
                //var contextTarefa = scopeTarefa.ServiceProvider
                //    .GetService<TarefaDbContext>();
                //ArgumentNullException.ThrowIfNull(contextTarefa);

                //contextIdentity.Database.EnsureDeleted();
                //contextTarefa.Database.EnsureDeleted();

                //contextIdentity.Database.EnsureCreated();
                //contextTarefa.Database.EnsureCreated();                
            });            

            base.ConfigureWebHost(builder);
        }
    }
}
