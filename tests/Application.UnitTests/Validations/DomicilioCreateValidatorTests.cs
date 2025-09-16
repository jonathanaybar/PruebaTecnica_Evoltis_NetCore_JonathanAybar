using Application.DTOs.Domicilio;
using Application.Validations;
using FluentAssertions;
using Xunit;

namespace Application.UnitTests.Validations;

public class DomicilioCreateValidatorTests
{
    private readonly DomicilioCreateValidator _validator = new();

    [Fact]
    public void EmptyProvincia_Should_Fail()
    {
        var dto = new DomicilioCreateRequestDto
        {
            Provincia = "",
            Ciudad = "Mendoza",
            Calle = "Mitre",
            Numero = "100"
        };

        var result = _validator.Validate(dto);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName.Contains(nameof(DomicilioCreateRequestDto.Provincia)));
    }

    [Fact]
    public void Valid_Should_Pass()
    {
        var dto = new DomicilioCreateRequestDto
        {
            Provincia = "Mendoza",
            Ciudad = "Mendoza",
            Calle = "Mitre",
            Numero = "100"
        };

        var result = _validator.Validate(dto);

        result.IsValid.Should().BeTrue(result.ToString());
    }
}
