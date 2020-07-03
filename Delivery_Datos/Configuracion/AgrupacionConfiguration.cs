using Delivery_Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Delivery_Datos.Configuracion
{
    public class AgrupacionConfiguration : IEntityTypeConfiguration<Agrupacion>
    {
        public void Configure(EntityTypeBuilder<Agrupacion> entity)
        {
           
                entity.HasKey(e => e.Codigo)
                    .HasName("PRIMARY");

                entity.ToTable("agrupacion");

                entity.HasIndex(e => e.CodigoPadreAgrupacion)
                    .HasName("fk_Agrupacion_Agrupacion1_idx");

                entity.Property(e => e.Codigo)
                    .HasColumnType("varchar(6)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Activo)
                    .IsRequired()
                    .HasColumnType("varchar(2)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.CantidadDeAgrupacionesHijo).HasColumnType("int(11)");

                entity.Property(e => e.CodigoPadreAgrupacion)
                    .HasColumnType("varchar(6)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.MenuPrincipal)
                    .IsRequired()
                    .HasColumnType("varchar(2)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.UrlImagen)
                    .HasColumnType("varchar(250)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

             

            //entity.HasOne(d => d.CodigoPadreAgrupacionNavigation)
            //     // .WithMany(p => p.InverseCodigoPadreAgrupacionNavigation)
            //     .HasForeignKey(d => d.CodigoPadreAgrupacion)
            //     .OnDelete(DeleteBehavior.ClientSetNull)
            //     .HasConstraintName("fk_Agrupacion_Agrupacion1");

        }
    }
}
