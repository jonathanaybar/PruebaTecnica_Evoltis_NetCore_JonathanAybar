using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class Usuario : IEntity
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime FechaCreacion { get; set; }
    public List<Domicilio> Domicilios { get; set; } = new();

    public void ActualizarEmail(string nuevoEmail)
    {
        if (string.IsNullOrWhiteSpace(nuevoEmail))
            throw new ArgumentException("El email no puede estar vacío.");

        Email = nuevoEmail;
    }
}
