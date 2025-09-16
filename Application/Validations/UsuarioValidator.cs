using Application.DTOs.Domicilio;
using Application.DTOs.Usuario;
using FluentValidation;

namespace Application.Validations
{
    public class UsuarioCreateValidator : AbstractValidator<UsuarioCreateRequestDto>
    {
        public UsuarioCreateValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre es requerido.")
                .MaximumLength(50);

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("El email es requerido.")
                .EmailAddress().WithMessage("El email no tiene un formato válido.")
                .MaximumLength(50);

            // Valida cada domicilio si fue enviado
            RuleForEach(x => x.Domicilios)
                .SetValidator(new DomicilioCreateValidator())
                .When(x => x.Domicilios != null).WithMessage("Domicilio/s invalido/s.");
        }
    }

    public class UsuarioUpdateValidator : AbstractValidator<UsuarioUpdateRequestDto>
    {
        public UsuarioUpdateValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre es requerido.")
                .MaximumLength(50);

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("El email es requerido.")
                .EmailAddress().WithMessage("El email no tiene un formato válido.")
                .MaximumLength(50);

            When(x => x.Domicilios is { Count: > 0 }, () =>
            {
                RuleForEach(x => x.Domicilios!)
                    .SetValidator(new DomicilioUpdateValidator());
            });
        }
    }
}
