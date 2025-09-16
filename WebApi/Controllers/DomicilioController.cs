using Application.DTOs.Domicilio;
using Application.Services.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

public class DomicilioController
  : BaseController<DomicilioCreateRequestDto, DomicilioUpdateRequestDto, DomicilioResponseDto, int>
{
    public DomicilioController(
        IDomicilioService service,
        IValidator<DomicilioCreateRequestDto> createValidator,
        IValidator<DomicilioUpdateRequestDto> updateValidator
    ) : base(service, createValidator, updateValidator) { }

    [HttpGet("{id:int}")]
    public override Task<ActionResult<DomicilioResponseDto>> GetById([FromRoute] int id, CancellationToken ct)
    => base.GetById(id, ct);

}
