using System;
using System.Collections.Generic;
using System.Text;
using Delivery_Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Delivery_Datos.Configuracion
{
    public class PersonaConfiguration : IEntityTypeConfiguration<Persona>
    {
        public void Configure(EntityTypeBuilder<Persona> entity)
        {
            entity.ToTable("persona");

            entity.HasIndex(e => e.Email)
                .HasName("Email_UNIQUE")
                .IsUnique();

            entity.HasIndex(e => e.NumeroDeDocumento)
                .HasName("NumeroDeDocumento_UNIQUE")
                .IsUnique();

            entity.HasIndex(e => e.Telefono)
                .HasName("Telefono_UNIQUE")
                .IsUnique();

            entity.HasIndex(e => e.TipoDeDocumentoCodigo)
                .HasName("fk_Persona_TipoDeDocumento1_idx");

            entity.Property(e => e.Id).HasColumnType("int(11)");

            entity.Property(e => e.CodigoDeVerificacion)
                .IsRequired()
                .HasColumnType("varchar(250)")
                .HasCharSet("utf8")
                .HasCollation("utf8_general_ci");

            entity.Property(e => e.Denominacion)
                .IsRequired()
                .HasColumnType("varchar(100)")
                .HasCharSet("utf8")
                .HasCollation("utf8_general_ci");

            entity.Property(e => e.Email)
                .HasColumnType("varchar(100)")
                .HasCharSet("utf8")
                .HasCollation("utf8_general_ci");

            entity.Property(e => e.NumeroDeDocumento)
                .HasColumnType("varchar(15)")
                .HasCharSet("utf8")
                .HasCollation("utf8_general_ci");

            entity.Property(e => e.Password)
                .IsRequired()
                .HasColumnType("varchar(250)")
                .HasCharSet("utf8")
                .HasCollation("utf8_general_ci");

            entity.Property(e => e.Telefono)
                .HasColumnType("varchar(15)")
                .HasCharSet("utf8")
                .HasCollation("utf8_general_ci");

            entity.Property(e => e.TelefonoVerificado)
                .IsRequired()
                .HasColumnType("varchar(2)")
                .HasCharSet("utf8")
                .HasCollation("utf8_general_ci");

            entity.Property(e => e.TipoDeDocumentoCodigo)
                .IsRequired()
                .HasColumnName("TipoDeDocumento_Codigo")
                .HasColumnType("varchar(3)")
                .HasCharSet("utf8")
                .HasCollation("utf8_general_ci");

            entity.HasOne(d => d.TipoDeDocumentoCodigoNavigation)
                .WithMany(p => p.Persona)
                .HasForeignKey(d => d.TipoDeDocumentoCodigo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Persona_TipoDeDocumento1");
        }
    }
}
