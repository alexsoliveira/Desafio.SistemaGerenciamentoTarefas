using Desafio.WebAPI.Core.Services.Identidade;

namespace Desafio.Identidade.Api.Configurations
{
    public static class ApiConfig
    {
        public static IServiceCollection AddApiConfiguration(this IServiceCollection services)
        {
            services.AddControllers();

            return services;
        }

        public static IApplicationBuilder UseApiConfiguration(this IApplicationBuilder app)
        {
            

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthConfiguration();
           

            return app;
        }
    }
}