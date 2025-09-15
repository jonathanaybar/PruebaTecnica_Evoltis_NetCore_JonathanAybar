using Application.Mapping;
using Application.Services.Implementations;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Interfaces;
using Infrastructure.Repositories;

namespace WebApi.Extensions
{
    //Aqui se agregan las Inyeciones de dependencias para emprolijar el codigo en el Program.cs

    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IDomicilioRepository, DomicilioRepository>();

            return services;
        }

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Services
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IDomicilioService, DomicilioService>();

            // AutoMapper
            services.AddAutoMapper(typeof(DtoProfile).Assembly);

            // FluentValidation
            //services.AddValidatorsFromAssembly(typeof(Application.DependencyInjection).Assembly);

            return services;
        }        
    }

}
