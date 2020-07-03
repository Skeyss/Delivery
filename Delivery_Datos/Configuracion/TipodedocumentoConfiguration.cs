using System;
using System.Collections.Generic;
using System.Text;
using Delivery_Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Delivery_Datos.Configuracion
{
    public class TipodedocumentoConfiguration : IEntityTypeConfiguration<Tipodedocumento>
    {
        public void Configure(EntityTypeBuilder<Tipodedocumento> entity)
        {
            entity.HasKey(e => e.Codigo)
                    .HasName("PRIMARY");

            entity.ToTable("tipodedocumento");

            entity.Property(e => e.Codigo)
                .HasColumnType("varchar(3)")
                .HasCharSet("utf8")
                .HasCollation("utf8_general_ci");

            entity.Property(e => e.Nombre)
                .IsRequired()
                .HasColumnType("varchar(50)")
                .HasCharSet("utf8")
                .HasCollation("utf8_general_ci");
        }
    }
}
