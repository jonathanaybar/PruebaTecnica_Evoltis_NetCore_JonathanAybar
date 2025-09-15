using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Models
{
    [Table("domicilio")]
    [Index("UsuarioID", Name = "FK_Usuario")]
    public partial class domicilio
    {
        [Key]
        [Column(TypeName = "int(11)")]
        public int Id { get; set; }
        [Column(TypeName = "int(11)")]
        public int UsuarioID { get; set; }
        [StringLength(50)]
        public string Calle { get; set; } = null!;
        [StringLength(50)]
        public string Numero { get; set; } = null!;
        [StringLength(50)]
        public string Provincia { get; set; } = null!;
        [StringLength(50)]
        public string Ciudad { get; set; } = null!;
        [Column(TypeName = "datetime")]
        public DateTime FechaCreacion { get; set; }

        [ForeignKey("UsuarioID")]
        [InverseProperty("domicilios")]
        public virtual usuario Usuario { get; set; } = null!;
    }
}
