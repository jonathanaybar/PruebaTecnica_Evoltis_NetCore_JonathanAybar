using Application.DTOs.Domicilio;
using Application.DTOs.Usuario;
using FluentValidation;

namespace Application.Validations
{
    public class UsuarioCreateValidator : AbstractValidator<UsuarioCreateRequestDto>
    {
        public UsuarioCreateValidator()
        {
            RuleFor(x => x.Nombre)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(50);

            // Valida cada domicilio si fue enviado
            RuleForEach(x => x.Domicilios)
                .SetValidator(new DomicilioCreateValidator())
                .When(x => x.Domicilios != null);
        }
    }

    public class UsuarioUpdateValidator : AbstractValidator<UsuarioUpdateRequestDto>
    {
        public UsuarioUpdateValidator()
        {
            RuleFor(x => x.Nombre)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(50);
        }
    }
}
