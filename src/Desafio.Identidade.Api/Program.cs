using Desafio.Identidade.Api.Configurations;


namespace Desafio.Identidade.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services
                .AddIdentityConfiguration(builder.Configuration)
                .AddApiConfiguration()
                .AddSwaggerConfiguration();            
            
            var app = builder.Build();
            app.UseSwaggerConfiguration();
            app.UseHttpsRedirection();            
            app.UseApiConfiguration();            
            app.MapControllers();                      

            app.Run();
        }
    }
}
