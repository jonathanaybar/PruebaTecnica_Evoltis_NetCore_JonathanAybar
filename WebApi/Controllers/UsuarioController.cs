using Application.DTOs.Usuario;
using Application.Services.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

public class UsuarioController
  : BaseController<UsuarioCreateRequestDto, UsuarioUpdateRequestDto, UsuarioResponseDto, int>
{
    private readonly IUsuarioService _usuarioService;

    public UsuarioController(
        IUsuarioService usuarioService,
        IValidator<UsuarioCreateRequestDto> createValidator,
        IValidator<UsuarioUpdateRequestDto> updateValidator
    ) : base(usuarioService, createValidator, updateValidator)
    {
        _usuarioService = usuarioService;
    }

    // GET api/usuario/{id:int}
    [HttpGet("{id:int}")]
    public override Task<ActionResult<UsuarioResponseDto>> GetById([FromRoute] int id, CancellationToken ct)
        => base.GetById(id, ct);

    // GET api/usuario/search?nombre=&provincia=&ciudad=
    [HttpGet("search")]
    public async Task<ActionResult<List<UsuarioResponseDto>>> Search(
        [FromQuery] string? nombre,
        [FromQuery] string? provincia,
        [FromQuery] string? ciudad,
        CancellationToken ct)
    {
        var r = await _usuarioService.SearchAsync(nombre, provincia, ciudad, ct);
        return Ok(r);
    }
}
