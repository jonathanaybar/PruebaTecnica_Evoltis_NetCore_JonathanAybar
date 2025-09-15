using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class DomicilioConfig : IEntityTypeConfiguration<domicilio>
{
    public void Configure(EntityTypeBuilder<domicilio> entity)
    {
        entity.ToTable("domicilio");

        entity.HasKey(x => x.Id);

        entity.Property(x => x.Id).HasColumnType("int(11)");

        entity.Property(x => x.UsuarioID).HasColumnName("UsuarioID").HasColumnType("int(11)");

        entity.Property(e => e.Calle).HasMaxLength(50);

        entity.Property(e => e.Numero).HasMaxLength(50);

        entity.Property(e => e.Provincia).HasMaxLength(50);

        entity.Property(e => e.Ciudad).HasMaxLength(50);

        entity.Property(e => e.FechaCreacion)
              .HasColumnType("datetime")
              .ValueGeneratedOnAdd()
              .HasDefaultValueSql("current_timestamp()");

        entity.Navigation(u => u.Usuario).AutoInclude();
    }
}
