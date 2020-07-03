using Delivery_Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Delivery_Datos.Configuracion
{
    public class PantalladebienvenidaConfiguration : IEntityTypeConfiguration<Pantalladebienvenida>
    {
        public void Configure(EntityTypeBuilder<Pantalladebienvenida> entity)
        {
            entity.ToTable("pantalladebienvenida");

            entity.Property(e => e.Id).HasColumnType("int(11)");

            entity.Property(e => e.MensajePrincipal)
                .HasColumnType("varchar(250)")
                .HasCharSet("utf8")
                .HasCollation("utf8_general_ci");

            entity.Property(e => e.MensajeSecundario)
                .HasColumnType("varchar(250)")
                .HasCharSet("utf8")
                .HasCollation("utf8_general_ci");

            entity.Property(e => e.OrdenDeVisualizacion).HasColumnType("int(11)");

            entity.Property(e => e.UrlImagen)
                .HasColumnType("varchar(250)")
                .HasCharSet("utf8")
                .HasCollation("utf8_general_ci");
        }
    }
}
