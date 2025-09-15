using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Usuario
{
    public record class UsuarioResponseDto
    {
        public int Id { get; init; }
        public string Nombre { get; init; } = string.Empty;
        public string Email { get; init; } = string.Empty;
        public DateTime FechaCreacion { get; init; }

        // Evitamos nulls en la salida
        public List<Domicilio.DomicilioResponseDto> Domicilios { get; init; } = new();
    }
}

