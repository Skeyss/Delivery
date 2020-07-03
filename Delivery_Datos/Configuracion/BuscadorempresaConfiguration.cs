using Delivery_Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Delivery_Datos.Configuracion
{
    public class BuscadorempresaConfiguration : IEntityTypeConfiguration<Buscadorempresa>
    {
        public void Configure(EntityTypeBuilder<Buscadorempresa> entity)
        {
            entity.ToTable("buscadorempresa");

            entity.HasIndex(e => e.EmpresaCodigo)
                .HasName("fk_BuscadorEmpresa_Empresa1_idx");

            entity.Property(e => e.Id)
                .HasColumnName("id")
                .HasColumnType("int(11)");

            entity.Property(e => e.EmpresaCodigo)
                .IsRequired()
                .HasColumnName("Empresa_Codigo")
                .HasColumnType("varchar(15)")
                .HasCharSet("utf8")
                .HasCollation("utf8_general_ci");

            entity.Property(e => e.PalabraClave)
                .IsRequired()
                .HasColumnType("varchar(15)")
                .HasCharSet("utf8")
                .HasCollation("utf8_general_ci");

            entity.HasOne(d => d.EmpresaCodigoNavigation)
                .WithMany(p => p.Buscadorempresa)
                .HasForeignKey(d => d.EmpresaCodigo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_BuscadorEmpresa_Empresa1");
        }
    }
}
