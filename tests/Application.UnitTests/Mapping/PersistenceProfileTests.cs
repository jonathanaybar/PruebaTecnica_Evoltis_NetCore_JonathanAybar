using AutoMapper;
using Domain.Entities;
using FluentAssertions;
using Infrastructure.Mapping;
using InfraUsuario = Infrastructure.Models.usuario;
using InfraDomicilio = Infrastructure.Models.domicilio;
using Xunit;

namespace Application.UnitTests.Mapping;

public class PersistenceProfileTests
{
    private readonly IMapper _mapper;

    public PersistenceProfileTests()
    {
        var cfg = new MapperConfiguration(c =>
        {
            c.AddProfile<PersistenceProfile>();
        });
        _mapper = cfg.CreateMapper();
    }

    [Fact]
    public void Should_Map_DomainUsuario_To_InfrastructureUsuario_And_Back()
    {
        var domain = new Usuario
        {
            Id = 1,
            Nombre = "Test",
            Email = "test@acme.com",
            FechaCreacion = DateTime.UtcNow,
            Domicilios = new List<Domicilio>
            {
                new Domicilio
                {
                    Id = 11,
                    Provincia = "Santa Fe",
                    Ciudad = "Rosario",
                    Calle = "Belgrano",
                    Numero = "100"
                }
            }
        };

        var infra = _mapper.Map<InfraUsuario>(domain);
        var back = _mapper.Map<Usuario>(infra);

        infra.Should().NotBeNull();
        infra.Email.Should().Be(domain.Email);
        infra.domicilios.Should().HaveCount(1);

        back.Email.Should().Be(domain.Email);
        back.Domicilios.Should().HaveCount(1);
        back.Domicilios.First().Ciudad.Should().Be("Rosario");
    }

    [Fact]
    public void Should_Map_DomainDomicilio_To_InfrastructureDomicilio_And_Back()
    {
        var dom = new Domicilio
        {
            Id = 5,
            Provincia = "Tucumán",
            Ciudad = "SMT",
            Calle = "9 de Julio",
            Numero = "100"
        };

        var infra = _mapper.Map<InfraDomicilio>(dom);
        var back = _mapper.Map<Domicilio>(infra);

        infra.Provincia.Should().Be(dom.Provincia);
        back.Ciudad.Should().Be(dom.Ciudad);
    }
}
