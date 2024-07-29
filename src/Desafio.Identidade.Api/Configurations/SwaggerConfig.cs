using Microsoft.OpenApi.Models;

namespace Desafio.Identidade.Api.Configurations
{
    public static class SwaggerConfig
    {
        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = "Desafio Identidade API",                                                            
                });

            });

            return services;
        }

        
        public static WebApplication UseSwaggerConfiguration(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            return app;                    
        }
    }
}