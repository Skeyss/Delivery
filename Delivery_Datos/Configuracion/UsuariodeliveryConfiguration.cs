using System;
using System.Collections.Generic;
using System.Text;
using Delivery_Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Delivery_Datos.Configuracion
{
    public class UsuariodeliveryConfiguration : IEntityTypeConfiguration<Usuariodelivery>
    {
        public void Configure(EntityTypeBuilder<Usuariodelivery> entity)
        {
            entity.ToTable("usuariodelivery");

            entity.HasIndex(e => e.EmpresaCodigo)
                .HasName("fk_UsuarioDelivery_Empresa1_idx");

            entity.HasIndex(e => e.TipoDeDocumentoCodigo)
                .HasName("fk_UsuarioDelivery_TipoDeDocumento1_idx");

            entity.Property(e => e.Id).HasColumnType("int(11)");

            entity.Property(e => e.CodigoDeVerificacion)
                .IsRequired()
                .HasColumnType("varchar(15)")
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

            entity.Property(e => e.EmpresaCodigo)
                .IsRequired()
                .HasColumnName("Empresa_Codigo")
                .HasColumnType("varchar(15)")
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

            entity.Property(e => e.TipoDeDocumentoCodigo)
                .IsRequired()
                .HasColumnName("TipoDeDocumento_Codigo")
                .HasColumnType("varchar(3)")
                .HasCharSet("utf8")
                .HasCollation("utf8_general_ci");

            entity.HasOne(d => d.EmpresaCodigoNavigation)
                .WithMany(p => p.Usuariodelivery)
                .HasForeignKey(d => d.EmpresaCodigo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_UsuarioDelivery_Empresa1");

            entity.HasOne(d => d.TipoDeDocumentoCodigoNavigation)
                .WithMany(p => p.Usuariodelivery)
                .HasForeignKey(d => d.TipoDeDocumentoCodigo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_UsuarioDelivery_TipoDeDocumento1");
        }
    }
}
