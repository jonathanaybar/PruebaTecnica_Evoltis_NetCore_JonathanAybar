using Application.DTOs.Domicilio;
using FluentValidation;

namespace Application.Validations
{
    public class DomicilioCreateValidator : AbstractValidator<DomicilioCreateRequestDto>
    {
        public DomicilioCreateValidator()
        {
            RuleFor(x => x.Calle)
                .NotEmpty().WithMessage("La calle es requerida.")
                .MaximumLength(50);

            RuleFor(x => x.Numero)
                .NotEmpty().WithMessage("El número es requerido.")
                .MaximumLength(50);

            RuleFor(x => x.Provincia)
                .NotEmpty().WithMessage("La provincia es requerida.")
                .MaximumLength(50);

            RuleFor(x => x.Ciudad)
                .NotEmpty().WithMessage("La ciudad es requerida.")
                .MaximumLength(50);
        }
    }
    public class DomicilioUpdateValidator : AbstractValidator<DomicilioUpdateRequestDto>
    {
        public DomicilioUpdateValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.Id)
            .Must(id => id > 0)
            .WithMessage("El Id del domicilio debe ser mayor a 0 cuando se especifica.");

            RuleFor(x => x.Calle)
                .NotEmpty().WithMessage("La calle es requerida.")
                .MaximumLength(50);

            RuleFor(x => x.Numero)
                .NotEmpty().WithMessage("El número es requerido.")
                .MaximumLength(50);

            RuleFor(x => x.Provincia)
                .NotEmpty().WithMessage("La provincia es requerida.")
                .MaximumLength(50);

            RuleFor(x => x.Ciudad)
                .NotEmpty().WithMessage("La ciudad es requerida.")
                .MaximumLength(50);
        }
    }
}
