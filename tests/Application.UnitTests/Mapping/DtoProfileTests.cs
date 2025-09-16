using AutoMapper;
using Application.DTOs.Domicilio;
using Application.DTOs.Usuario;
using Application.Mapping;
using Domain.Entities;
using FluentAssertions;
using Xunit;

namespace Application.UnitTests.Mapping;

public class DtoProfileTests
{
    private readonly IMapper _mapper;

    public DtoProfileTests()
    {
        var cfg = new MapperConfiguration(c =>
        {
            c.AddProfile<DtoProfile>();
        });
        _mapper = cfg.CreateMapper();
    }

    [Fact]
    public void Should_Map_UsuarioCreate_To_DomainUsuario()
    {
        var dto = new UsuarioCreateRequestDto
        {
            Nombre = "Ana",
            Email = "ana@test.com",
            Domicilios = new List<DomicilioCreateRequestDto>
            {
                new DomicilioCreateRequestDto
                {
                    Provincia = "Buenos Aires",
                    Ciudad = "Lanús",
                    Calle = "Rivadavia",
                    Numero = "100"
                }
            }
        };

        var entity = _mapper.Map<Usuario>(dto);

        entity.Should().NotBeNull();
        entity.Nombre.Should().Be("Ana");
        entity.Email.Should().Be("ana@test.com");
        entity.Domicilios.Should().HaveCount(1);
        entity.Domicilios.First().Provincia.Should().Be("Buenos Aires");
    }

    [Fact]
    public void Should_Map_DomainUsuario_To_UsuarioResponseDto()
    {
        var entity = new Usuario
        {
            Id = 42,
            Nombre = "Pepe",
            Email = "pepe@test.com",
            FechaCreacion = DateTime.UtcNow.AddDays(-1),
            Domicilios = new List<Domicilio>
            {
                new Domicilio
                {
                    Id = 1,
                    Provincia = "Córdoba",
                    Ciudad = "Córdoba",
                    Calle = "Sarmiento",
                    Numero = "100"
                }
            }
        };

        var dto = _mapper.Map<UsuarioResponseDto>(entity);

        dto.Id.Should().Be(42);
        dto.Nombre.Should().Be("Pepe");
        dto.Email.Should().Be("pepe@test.com");
        dto.Domicilios.Should().HaveCount(1);
        dto.Domicilios.First().Provincia.Should().Be("Córdoba");
    }
}
