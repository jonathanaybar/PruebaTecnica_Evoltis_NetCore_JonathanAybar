using Application.DTOs;
using Application.DTOs.Domicilio;
using Application.Services.Interfaces;     
using FluentValidation;

public class DomicilioController
  : BaseController<DomicilioCreateRequestDto, DomicilioUpdateRequestDto, DomicilioResponseDto, int>
{
    public DomicilioController(
        IDomicilioService service,
        IValidator<DomicilioCreateRequestDto> createValidator,
        IValidator<DomicilioUpdateRequestDto> updateValidator
    ) : base(service, createValidator, updateValidator)
    { }
}

