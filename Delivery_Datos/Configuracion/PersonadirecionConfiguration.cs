using System;
using System.Collections.Generic;
using System.Text;
using Delivery_Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Delivery_Datos.Configuracion
{
    public class PersonadirecionConfiguration : IEntityTypeConfiguration<Personadirecion>
    {
        public void Configure(EntityTypeBuilder<Personadirecion> entity)
        {
            entity.HasNoKey();

            entity.ToTable("personadirecion");

            entity.HasIndex(e => e.PersonaId)
                .HasName("fk_PersonaDirecion_Persona1_idx");

            entity.Property(e => e.Direccion)
                .IsRequired()
                .HasColumnType("varchar(250)")
                .HasCharSet("utf8")
                .HasCollation("utf8_general_ci");

            entity.Property(e => e.PersonaId)
                .HasColumnName("Persona_Id")
                .HasColumnType("int(11)");

            entity.HasOne(d => d.Persona)
                .WithMany()
                .HasForeignKey(d => d.PersonaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_PersonaDirecion_Persona1");
        }
    }
}
