using Application.DTOs.Domicilio;
using Application.DTOs.Usuario;
using Application.Validations;
using FluentAssertions;
using Xunit;

namespace Application.UnitTests.Validations;

public class UsuarioCreateValidatorTests
{
    private readonly UsuarioCreateValidator _validator = new();

    [Fact]
    public void InvalidEmail_Should_Fail()
    {
        var dto = new UsuarioCreateRequestDto
        {
            Nombre = "Juan",
            Email = "no-es-un-email",
            Domicilios = new List<DomicilioCreateRequestDto>()
        };

        var result = _validator.Validate(dto);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName.Contains(nameof(UsuarioCreateRequestDto.Email)));
    }

    [Fact]
    public void EmptyNombre_Should_Fail()
    {
        var dto = new UsuarioCreateRequestDto
        {
            Nombre = "",
            Email = "juan@test.com",
            Domicilios = new List<DomicilioCreateRequestDto>()
        };

        var result = _validator.Validate(dto);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName.Contains(nameof(UsuarioCreateRequestDto.Nombre)));
    }

    [Fact]
    public void InvalidNestedDomicilio_Should_Fail()
    {
        var dto = new UsuarioCreateRequestDto
        {
            Nombre = "Juan",
            Email = "juan@test.com",
            Domicilios = new List<DomicilioCreateRequestDto>
            {
                new DomicilioCreateRequestDto
                {
                    Provincia = "", 
                    Ciudad = "",
                    Calle = "",
                    Numero = "",
                }
            }
        };

        var result = _validator.Validate(dto);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName.Contains("Domicilios[0].Provincia"));
    }

    [Fact]
    public void Valid_Should_Pass()
    {
        var dto = new UsuarioCreateRequestDto
        {
            Nombre = "Juan",
            Email = "juan@test.com",
            Domicilios = new List<DomicilioCreateRequestDto>
            {
                new DomicilioCreateRequestDto
                {
                    Provincia = "Córdoba",
                    Ciudad = "Córdoba",
                    Calle = "San Martín",
                    Numero = "123",
                }
            }
        };

        var result = _validator.Validate(dto);

        result.IsValid.Should().BeTrue(result.ToString());
    }
}
