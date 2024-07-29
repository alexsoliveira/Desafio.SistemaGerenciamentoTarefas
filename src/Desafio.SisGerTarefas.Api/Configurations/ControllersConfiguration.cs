using Desafio.SisGerTarefas.Api.Filters;
using Microsoft.OpenApi.Models;

namespace Desafio.SisGerTarefas.Api.Configurations
{
    public static class ControllersConfiguration
    {
        public static IServiceCollection AddAndConfigureControllers(
            this IServiceCollection services
        )
        {
            services.AddControllers(options
                => options.Filters.Add(typeof(ApiGlobalExceptionFilter))
            );
            services.AddDocumentation();

            return services;
        }

        private static IServiceCollection AddDocumentation(
            this IServiceCollection services
        )
        {
            services.AddEndpointsApiExplorer();            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = "Desafio Sistema de Gerenciamento de Tarefas API",
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Insira o token JWT desta maneira: Bearer {seu token}",
                    Name = "Authorization",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }                        
                        },
                        new string[] { }
                    }
                });

            });
            return services;
        }

        public static WebApplication UseDocumentation(
            this WebApplication app
        )
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

