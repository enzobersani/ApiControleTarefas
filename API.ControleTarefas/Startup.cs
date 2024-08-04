using API.ControleTarefas.Domain.Interfaces.Repositories;
using API.ControleTarefas.Domain.Interfaces.UnitOfWork;
using API.ControleTarefas.Infrastructure.Repositories;
using API.ControleTarefas.Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using System.Runtime.CompilerServices;

namespace API.ControleTarefas
{
    public static class Startup
    {
        public static void ConfigureRepositories(IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }

        public static IServiceCollection AddInfrastructureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Token JWT"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme()
                        {
                            Reference = new OpenApiReference()
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });

            return services;
        }
    }
}
