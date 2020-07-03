using System;
using System.Collections.Generic;
using System.Text;
using Delivery_Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Delivery_Datos.Configuracion
{
    public class PedidoConfiguration : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> entity)
        {
            entity.ToTable("pedido");

            entity.HasIndex(e => e.EmpresaCodigo)
                .HasName("fk_Pedido_Empresa1_idx");

            entity.HasIndex(e => e.PersonaId)
                .HasName("fk_Pedido_Persona1_idx");

            entity.HasIndex(e => e.SucursalId)
                .HasName("fk_Pedido_Sucursal1_idx");

            entity.Property(e => e.Id).HasColumnType("int(11)");

            entity.Property(e => e.Denominacion)
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

            entity.Property(e => e.Estado)
                .IsRequired()
                .HasColumnType("varchar(50)")
                .HasCharSet("utf8")
                .HasCollation("utf8_general_ci");

            entity.Property(e => e.FechaYhoraDeAprovacion).HasColumnName("FechaYHoraDeAprovacion");

            entity.Property(e => e.FechaYhoraDeEntrega).HasColumnName("FechaYHoraDeEntrega");

            entity.Property(e => e.FechaYhoraDeRegistro).HasColumnName("FechaYHoraDeRegistro");

            entity.Property(e => e.FechaYhoraDeSalida).HasColumnName("FechaYHoraDeSalida");

            entity.Property(e => e.FechaYhoraProgramadaDeEnvio)
                .IsRequired()
                .HasColumnName("FechaYHoraProgramadaDeEnvio")
                .HasColumnType("varchar(45)")
                .HasCharSet("utf8")
                .HasCollation("utf8_general_ci");

            entity.Property(e => e.IdMotorizadoAprobacion)
                .HasColumnName("idMotorizadoAprobacion")
                .HasColumnType("int(11)");

            entity.Property(e => e.IdMotorizadoEntrega)
                .HasColumnName("idMotorizadoEntrega")
                .HasColumnType("int(11)");

            entity.Property(e => e.IdMotorizadoSalida)
                .HasColumnName("idMotorizadoSalida")
                .HasColumnType("int(11)");

            entity.Property(e => e.IdUsuarioAprobacion).HasColumnType("int(11)");

            entity.Property(e => e.IdUsuarioSalida)
                .HasColumnName("idUsuarioSalida")
                .HasColumnType("int(11)");

            entity.Property(e => e.IdUusarioEntrega)
                .HasColumnName("idUusarioEntrega")
                .HasColumnType("int(11)");

            entity.Property(e => e.NumeroDeDocumento)
                .HasColumnType("varchar(15)")
                .HasCharSet("utf8")
                .HasCollation("utf8_general_ci");

            entity.Property(e => e.PersonaId)
                .HasColumnName("Persona_Id")
                .HasColumnType("int(11)");

            entity.Property(e => e.SucursalId)
                .IsRequired()
                .HasColumnName("Sucursal_Id")
                .HasColumnType("varchar(15)")
                .HasCharSet("utf8")
                .HasCollation("utf8_general_ci");

            entity.Property(e => e.Telefono)
                .IsRequired()
                .HasColumnType("varchar(15)")
                .HasCharSet("utf8")
                .HasCollation("utf8_general_ci");

            entity.Property(e => e.Tipo)
                .HasColumnType("varchar(50)")
                .HasCharSet("utf8")
                .HasCollation("utf8_general_ci");

            entity.HasOne(d => d.EmpresaCodigoNavigation)
                .WithMany(p => p.Pedido)
                .HasForeignKey(d => d.EmpresaCodigo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Pedido_Empresa1");

            entity.HasOne(d => d.Persona)
                .WithMany(p => p.Pedido)
                .HasForeignKey(d => d.PersonaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Pedido_Persona1");

            entity.HasOne(d => d.Sucursal)
                .WithMany(p => p.Pedido)
                .HasForeignKey(d => d.SucursalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Pedido_Sucursal1");
        }
    }
}
