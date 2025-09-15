using Application.DTOs.Domicilio;
using FluentValidation;

namespace Application.Validations
{
    public class DomicilioCreateValidator : AbstractValidator<DomicilioCreateRequestDto>
    {
        public DomicilioCreateValidator()
        {
            RuleFor(x => x.UsuarioId).GreaterThan(0);
            RuleFor(x => x.Calle).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Numero).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Provincia).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Ciudad).NotEmpty().MaximumLength(50);
        }
    }
    public class DomicilioUpdateValidator : AbstractValidator<DomicilioUpdateRequestDto>
    {
        public DomicilioUpdateValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.Calle).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Numero).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Provincia).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Ciudad).NotEmpty().MaximumLength(50);
        }
    }
}
