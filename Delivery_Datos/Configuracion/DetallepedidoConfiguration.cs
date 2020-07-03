using System;
using System.Collections.Generic;
using System.Text;
using Delivery_Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Delivery_Datos.Configuracion
{
    public class DetallepedidoConfiguration : IEntityTypeConfiguration<Detallepedido>
    {
        public void Configure(EntityTypeBuilder<Detallepedido> entity)
        {
            entity.ToTable("detallepedido");

            entity.HasIndex(e => e.ItemCodigo)
                .HasName("fk_DetallePedido_Item1_idx");

            entity.HasIndex(e => e.PedidoId)
                .HasName("fk_DetallePedido_Pedido1_idx");

            entity.Property(e => e.Id).HasColumnType("int(11)");

            entity.Property(e => e.Descripcion)
                .HasColumnType("varchar(150)")
                .HasCharSet("utf8")
                .HasCollation("utf8_general_ci");

            entity.Property(e => e.Estado)
                .IsRequired()
                .HasColumnType("varchar(50)")
                .HasCharSet("utf8")
                .HasCollation("utf8_general_ci");

            entity.Property(e => e.ItemCodigo)
                .IsRequired()
                .HasColumnName("Item_Codigo")
                .HasColumnType("varchar(25)")
                .HasCharSet("utf8")
                .HasCollation("utf8_general_ci");

            entity.Property(e => e.Nombre)
                .IsRequired()
                .HasColumnType("varchar(50)")
                .HasCharSet("utf8")
                .HasCollation("utf8_general_ci");

            entity.Property(e => e.PedidoId)
                .HasColumnName("Pedido_Id")
                .HasColumnType("int(11)");

            entity.HasOne(d => d.ItemCodigoNavigation)
                .WithMany(p => p.Detallepedido)
                .HasForeignKey(d => d.ItemCodigo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_DetallePedido_Item1");

            entity.HasOne(d => d.Pedido)
                .WithMany(p => p.Detallepedido)
                .HasForeignKey(d => d.PedidoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_DetallePedido_Pedido1");
        }
    }
}
