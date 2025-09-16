using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Usuario
{
    public record class UsuarioCreateRequestDto
    {
        public string Nombre { get; init; } = string.Empty;
        public string Email { get; init; } = string.Empty;

        // Para permitir crear con domicilios
        public List<Domicilio.DomicilioCreateRequestDto>? Domicilios { get; init; }
    }

    public record class UsuarioUpdateRequestDto
    {
        public int? Id { get; init; }
        public string Nombre { get; init; } = string.Empty;
        public string Email { get; init; } = string.Empty;

        // Para reemplazar/agregar domicilios del usuario
        public List<Domicilio.DomicilioUpdateRequestDto>? Domicilios { get; init; }
    }

    public record class UsuarioSearchRequestDto
    {
        public string? Nombre { get; init; }
        public string? Provincia { get; init; }
        public string? Ciudad { get; init; }
    }
}

