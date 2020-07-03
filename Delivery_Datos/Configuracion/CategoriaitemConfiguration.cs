using Delivery_Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Delivery_Datos.Configuracion
{
    public class CategoriaitemConfiguration : IEntityTypeConfiguration<Categoriaitem>
    {
        public void Configure(EntityTypeBuilder<Categoriaitem> entity)
        {
            entity.HasNoKey();

            entity.ToTable("categoriaitem");

            entity.HasIndex(e => e.CategoriaCodigo)
                .HasName("fk_CategoriaItem_Categoria1_idx");

            entity.HasIndex(e => e.ItemCodigo)
                .HasName("fk_CategoriaItem_Item1_idx");

            entity.Property(e => e.CategoriaCodigo)
                .IsRequired()
                .HasColumnName("Categoria_Codigo")
                .HasColumnType("varchar(15)")
                .HasCharSet("utf8")
                .HasCollation("utf8_general_ci");

            entity.Property(e => e.ItemCodigo)
                .IsRequired()
                .HasColumnName("Item_Codigo")
                .HasColumnType("varchar(25)")
                .HasCharSet("utf8")
                .HasCollation("utf8_general_ci");

            entity.HasOne(d => d.CategoriaCodigoNavigation)
                .WithMany()
                .HasForeignKey(d => d.CategoriaCodigo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_CategoriaItem_Categoria1");

            entity.HasOne(d => d.ItemCodigoNavigation)
                .WithMany()
                .HasForeignKey(d => d.ItemCodigo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_CategoriaItem_Item1");
        }
    }
}
