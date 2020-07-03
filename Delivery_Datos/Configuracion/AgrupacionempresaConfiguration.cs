using Delivery_Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Delivery_Datos.Configuracion
{
    public class AgrupacionempresaConfiguration : IEntityTypeConfiguration<Agrupacionempresa>
    {
        public void Configure(EntityTypeBuilder<Agrupacionempresa> entity)
        {
            entity.HasNoKey();

            entity.ToTable("agrupacionempresa");

            entity.HasIndex(e => e.AgrupacionCodigo)
                .HasName("fk_AgrupacionEmpresa_Agrupacion1_idx");

            entity.HasIndex(e => e.EmpresaCodigo)
                .HasName("fk_AgrupacionEmpresa_Empresa1_idx");

            entity.Property(e => e.AgrupacionCodigo)
                .IsRequired()
                .HasColumnName("Agrupacion_Codigo")
                .HasColumnType("varchar(6)")
                .HasCharSet("utf8")
                .HasCollation("utf8_general_ci");

            entity.Property(e => e.EmpresaCodigo)
                .IsRequired()
                .HasColumnName("Empresa_Codigo")
                .HasColumnType("varchar(15)")
                .HasCharSet("utf8")
                .HasCollation("utf8_general_ci");

            entity.HasOne(d => d.AgrupacionCodigoNavigation)
                .WithMany()
                .HasForeignKey(d => d.AgrupacionCodigo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_AgrupacionEmpresa_Agrupacion1");

            entity.HasOne(d => d.EmpresaCodigoNavigation)
                .WithMany()
                .HasForeignKey(d => d.EmpresaCodigo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_AgrupacionEmpresa_Empresa1");
        }
    }
}
