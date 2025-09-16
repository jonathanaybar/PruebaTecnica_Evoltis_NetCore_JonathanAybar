using Application.DTOs;            // IHasId<TKey>
using Application.Services;        // IServiceBase<,,,>
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class BaseController<TCreate, TUpdate, TRead, TKey> : ControllerBase
    where TRead : IHasId<TKey>
{
    private readonly IServiceBase<TCreate, TUpdate, TRead, TKey> _service;
    private readonly IValidator<TCreate>? _createValidator;
    private readonly IValidator<TUpdate>? _updateValidator;

    public BaseController(
        IServiceBase<TCreate, TUpdate, TRead, TKey> service,
        IValidator<TCreate>? createValidator = null,
        IValidator<TUpdate>? updateValidator = null)
    {
        _service = service;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }

    // GET api/[controller]/{id}
    [HttpGet("{id}")]
    public virtual async Task<ActionResult<TRead>> GetById([FromRoute] TKey id, CancellationToken ct)
    {
        var r = await _service.GetByIdAsync(id, ct);
        return r is null ? NotFound() : Ok(r);
    }

    // GET api/[controller]
    [HttpGet]
    public virtual async Task<ActionResult<List<TRead>>> GetAll(CancellationToken ct)
    {
        var items = await _service.GetAllAsync(ct);
        return Ok(items);
    }

    // POST api/[controller]
    [HttpPost]
    public virtual async Task<ActionResult<TRead>> Create([FromBody] TCreate dto, CancellationToken ct)
    {
        if (_createValidator is not null)
        {
            var v = await _createValidator.ValidateAsync(dto, ct);
            if (!v.IsValid)
            {
                var pd = new ValidationProblemDetails();
                foreach (var kv in v.ToDictionary()) pd.Errors.Add(kv.Key, kv.Value);
                return ValidationProblem(pd);
            }
        }

        var created = await _service.CreateAsync(dto, ct);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    // PUT api/[controller]/{id}
    [HttpPut("{id}")]
    public virtual async Task<ActionResult<TRead>> Update([FromRoute] TKey id, [FromBody] TUpdate dto, CancellationToken ct)
    {
        if (_updateValidator is not null)
        {
            var v = await _updateValidator.ValidateAsync(dto, ct);
            if (!v.IsValid)
            {
                var pd = new ValidationProblemDetails();
                foreach (var kv in v.ToDictionary()) pd.Errors.Add(kv.Key, kv.Value);
                return ValidationProblem(pd);
            }
        }

        var r = await _service.UpdateAsync(id, dto, ct);
        return r is null ? NotFound() : Ok(r);
    }

    // DELETE api/[controller]/{id}
    [HttpDelete("{id}")]
    public virtual async Task<IActionResult> Delete([FromRoute] TKey id, CancellationToken ct)
    {
        var ok = await _service.DeleteAsync(id, ct);
        return ok ? NoContent() : NotFound();
    }
}
