using FluentValidation;
using FluentValidation.AspNetCore;

namespace WebApi.Extensions
{
    public static class ValidationExtensions
    {
        /// <summary>
        /// Registra FluentValidation (auto-validation) y todos los validators del assembly de Application.
        /// Compatible con FluentValidation.AspNetCore 10.3.6
        /// </summary>
        public static IServiceCollection AddApiValidations(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();

            services.AddValidatorsFromAssembly(typeof(Application.Validations.UsuarioCreateValidator).Assembly);

            return services;
        }
    }
}
