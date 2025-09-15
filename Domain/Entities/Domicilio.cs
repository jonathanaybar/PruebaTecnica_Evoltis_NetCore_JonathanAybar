using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Domicilio : IEntity
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string Calle { get; set; } = string.Empty;
        public string Numero { get; set; } = string.Empty;
        public string Provincia { get; set; } = string.Empty;
        public string Ciudad { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; }
        public Usuario? Usuario { get; set; }
    }
}
