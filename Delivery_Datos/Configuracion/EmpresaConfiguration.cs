using System;
using System.Collections.Generic;
using System.Text;
using Delivery_Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Delivery_Datos.Configuracion
{
    public class EmpresaConfiguration : IEntityTypeConfiguration<Empresa>
    {
        public void Configure(EntityTypeBuilder<Empresa> entity)
        {
            entity.HasKey(e => e.Codigo)
                    .HasName("PRIMARY");

            entity.ToTable("empresa");

            entity.HasIndex(e => e.TipoDeDocumentoCodigo)
                .HasName("fk_Empresa_TipoDeDocumento1_idx");

            entity.Property(e => e.Codigo)
                .HasColumnType("varchar(15)")
                .HasCharSet("utf8")
                .HasCollation("utf8_general_ci");

            entity.Property(e => e.MinutosMaxDelivery).HasColumnType("int(11)");

            entity.Property(e => e.MinutosMinDelivery).HasColumnType("int(11)");

            entity.Property(e => e.NombreComercial)
                .IsRequired()
                .HasColumnType("varchar(100)")
                .HasCharSet("utf8")
                .HasCollation("utf8_general_ci");

            entity.Property(e => e.RazonSocialDenominacion)
                .IsRequired()
                .HasColumnType("varchar(100)")
                .HasCharSet("utf8")
                .HasCollation("utf8_general_ci");

            entity.Property(e => e.TieneOfertaDelivery)
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

            entity.Property(e => e.UrlImagen)
                .HasColumnType("varchar(250)")
                .HasCharSet("utf8")
                .HasCollation("utf8_general_ci");

            entity.HasOne(d => d.TipoDeDocumentoCodigoNavigation)
                .WithMany(p => p.Empresa)
                .HasForeignKey(d => d.TipoDeDocumentoCodigo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Empresa_TipoDeDocumento1");
        }
    }
}
