using System;
using System.Collections.Generic;

public class UsuarioResponseLite
{
    public int Id { get; set; }
    public string Nombre { get; set; } = default!;
    public string Email { get; set; } = default!;
    public DateTime FechaCreacion { get; set; }
    public List<DomicilioLite> Domicilios { get; set; } = new();
}

public class DomicilioLite
{
    public int Id { get; set; }
    public string Calle { get; set; } = default!;
    public string Numero { get; set; } = default!;
    public string Provincia { get; set; } = default!;
    public string Ciudad { get; set; } = default!;
    public DateTime FechaCreacion { get; set; }
}

