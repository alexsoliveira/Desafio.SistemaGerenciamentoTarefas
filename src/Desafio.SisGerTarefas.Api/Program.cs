
using Desafio.SisGerTarefas.Api.Configurations;
using Desafio.WebAPI.Core.Services.Identidade;

namespace Desafio.SisGerTarefas.Api
{
    public partial class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services
                .AddAppConnections(builder.Configuration)
                .AddUseCases()
                .AddAndConfigureControllers();                        

            var app = builder.Build();
            app.UseDocumentation();
            app.UseHttpsRedirection();
            app.UseAuthConfiguration();
            app.MapControllers();

            app.Run();
        }        
    }
    public partial class Program { }
}
