using System;
using System.Collections.Generic;
using System.Text;
using Delivery_Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Delivery_Datos.Configuracion
{
    public class SucursalConfiguration : IEntityTypeConfiguration<Sucursal>
    {
        public void Configure(EntityTypeBuilder<Sucursal> entity)
        {
            entity.ToTable("sucursal");

            entity.HasIndex(e => e.EmpresaCodigo)
                .HasName("fk_Sucursal_Empresa1_idx");

            entity.Property(e => e.Id)
                .HasColumnType("varchar(15)")
                .HasCharSet("utf8")
                .HasCollation("utf8_general_ci");

            entity.Property(e => e.Direccion)
                .IsRequired()
                .HasColumnType("varchar(100)")
                .HasCharSet("utf8")
                .HasCollation("utf8_general_ci");

            entity.Property(e => e.EmpresaCodigo)
                .IsRequired()
                .HasColumnName("Empresa_Codigo")
                .HasColumnType("varchar(15)")
                .HasCharSet("utf8")
                .HasCollation("utf8_general_ci");

            entity.Property(e => e.Nombre)
                .IsRequired()
                .HasColumnType("varchar(100)")
                .HasCharSet("utf8")
                .HasCollation("utf8_general_ci");

            entity.HasOne(d => d.EmpresaCodigoNavigation)
                .WithMany(p => p.Sucursal)
                .HasForeignKey(d => d.EmpresaCodigo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Sucursal_Empresa1");
        }
    }
}
