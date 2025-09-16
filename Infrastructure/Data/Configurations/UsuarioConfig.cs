using Domain.Entities;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Infrastructure.Data.Configurations;

public class UsuarioConfig : IEntityTypeConfiguration<usuario>
{
    public void Configure(EntityTypeBuilder<usuario> entity)
    {

        entity.ToTable("usuario");

        entity.HasKey(e => e.Id);
        entity.Property(e => e.Id).ValueGeneratedOnAdd();

        entity.Property(e => e.Nombre).HasMaxLength(50);

        entity.Property(e => e.Email).HasMaxLength(50);

        entity.Property(e => e.FechaCreacion)
              .HasDefaultValueSql("CURRENT_TIMESTAMP");

        entity.HasMany(e => e.domicilios)
        .WithOne(d => d.Usuario!)
        .HasForeignKey(d => d.UsuarioID)
        .HasConstraintName("FK_Usuario");

        entity.Navigation(u => u.domicilios).AutoInclude();
    }
}
