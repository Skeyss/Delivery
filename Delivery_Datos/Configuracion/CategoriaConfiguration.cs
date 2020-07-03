using System;
using System.Collections.Generic;
using System.Text;
using Delivery_Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Delivery_Datos.Configuracion
{
    public class CategoriaConfiguration : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> entity)
        {
            entity.HasKey(e => e.Codigo)
                  .HasName("PRIMARY");

            entity.ToTable("categoria");

            entity.HasIndex(e => e.CodigoPadreAgrupacion)
                .HasName("fk_Categoria_Categoria1_idx");

            entity.HasIndex(e => e.EmpresaCodigo)
                .HasName("fk_Categoria_Empresa1_idx");

            entity.Property(e => e.Codigo)
                .HasColumnType("varchar(15)")
                .HasCharSet("utf8")
                .HasCollation("utf8_general_ci");

            entity.Property(e => e.Activo)
                .IsRequired()
                .HasColumnType("varchar(2)")
                .HasCharSet("utf8")
                .HasCollation("utf8_general_ci");

            entity.Property(e => e.CantidadDeCategoria)
                .IsRequired()
                .HasColumnType("varchar(45)")
                .HasCharSet("utf8")
                .HasCollation("utf8_general_ci");

            entity.Property(e => e.CodigoPadreAgrupacion)
                .IsRequired()
                .HasColumnType("varchar(15)")
                .HasCharSet("utf8")
                .HasCollation("utf8_general_ci");

            entity.Property(e => e.Descripcion)
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

            entity.Property(e => e.MenuPrincipal)
                .IsRequired()
                .HasColumnType("varchar(2)")
                .HasCharSet("utf8")
                .HasCollation("utf8_general_ci");

            entity.Property(e => e.Nombre)
                .IsRequired()
                .HasColumnType("varchar(100)")
                .HasCharSet("utf8")
                .HasCollation("utf8_general_ci");

            entity.Property(e => e.UrlImagen)
                .HasColumnType("varchar(250)")
                .HasCharSet("utf8")
                .HasCollation("utf8_general_ci");

            entity.HasOne(d => d.CodigoPadreAgrupacionNavigation)
                .WithMany(p => p.InverseCodigoPadreAgrupacionNavigation)
                .HasForeignKey(d => d.CodigoPadreAgrupacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Categoria_Categoria1");

            entity.HasOne(d => d.EmpresaCodigoNavigation)
                .WithMany(p => p.Categoria)
                .HasForeignKey(d => d.EmpresaCodigo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Categoria_Empresa1");
        }
    }
}
