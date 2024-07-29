using Desafio.Identidade.Api.Data;
using Desafio.Identidade.Api.Extensions;
using Desafio.WebAPI.Core.Services.Identidade;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Desafio.Identidade.Api.Configurations
{
    public static class IdentityConfig
    {
        public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddErrorDescriber<IdentityMensagensPortugues>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddJwtConfiguration(configuration);

            return services;
        }        
    }
}
