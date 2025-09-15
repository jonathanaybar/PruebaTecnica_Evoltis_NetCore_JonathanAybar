using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Domicilio
{
    public record class DomicilioResponseDto
    {
        public int Id { get; init; }
        public int UsuarioId { get; init; }
        public string Calle { get; init; } = string.Empty;
        public string Numero { get; init; } = string.Empty;
        public string Provincia { get; init; } = string.Empty;
        public string Ciudad { get; init; } = string.Empty;
        public DateTime FechaCreacion { get; init; }
    }
}
