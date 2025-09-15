using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Models
{
    [Table("usuario")]
    public partial class usuario
    {
        public usuario()
        {
            domicilios = new HashSet<domicilio>();
        }

        [Key]
        [Column(TypeName = "int(11)")]
        public int Id { get; set; }
        [StringLength(50)]
        public string? Nombre { get; set; }
        [StringLength(50)]
        public string? Email { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime FechaCreacion { get; set; }

        [InverseProperty("Usuario")]
        public virtual ICollection<domicilio> domicilios { get; set; }
    }
}
