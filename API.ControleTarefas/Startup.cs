using API.ControleTarefas.Domain.Commands;
using API.ControleTarefas.Domain.Interfaces.UnitOfWork;
using API.ControleTarefas.Domain.Validators;
using API.ControleTarefas.Infrastructure.UnitOfWork;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace API.ControleTarefas
{
    public static class Startup
    {
        public static void ConfigureRepositories(IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }

        public static void ConfigureValidators(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<LoginCommandValidator>();
            services.AddValidatorsFromAssemblyContaining<RegisterUserCommandValidator>();
            services.AddValidatorsFromAssemblyContaining<InsertProjectCommandValidator>();
            services.AddValidatorsFromAssemblyContaining<InsertTaskCommandValidator>();
            services.AddValidatorsFromAssemblyContaining<InsertCollaboratorCommandValidator>();
            services.AddValidatorsFromAssemblyContaining<InsertTimeTrackersCommandValidator>();
            services.AddValidatorsFromAssemblyContaining<SearchProjectByIdQueryValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdateProjectCommandValidator>();
            services.AddValidatorsFromAssemblyContaining<DeleteProjectCommandValidator>();
            services.AddValidatorsFromAssemblyContaining<DeleteTaskCommandValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdateTaskCommandValidator>();
            services.AddValidatorsFromAssemblyContaining<SearchHoursQueryValidator>();
        }

        public static IServiceCollection AddInfrastructureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API Controle Tarefas", Version = "v1" });

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

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath);

            });

            return services;
        }
    }
}
