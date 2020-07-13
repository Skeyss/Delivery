using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ClassLibrary1
{
    public partial class deliveryhuancayoContext : DbContext
    {
        public deliveryhuancayoContext()
        {
        }

        public deliveryhuancayoContext(DbContextOptions<deliveryhuancayoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Agrupacion> Agrupacion { get; set; }
        public virtual DbSet<Agrupacionempresa> Agrupacionempresa { get; set; }
        public virtual DbSet<Buscadorempresa> Buscadorempresa { get; set; }
        public virtual DbSet<Categoria> Categoria { get; set; }
        public virtual DbSet<Categoriaitem> Categoriaitem { get; set; }
        public virtual DbSet<Detallepedido> Detallepedido { get; set; }
        public virtual DbSet<Empresa> Empresa { get; set; }
        public virtual DbSet<Item> Item { get; set; }
        public virtual DbSet<Motorizado> Motorizado { get; set; }
        public virtual DbSet<Pantalladebienvenida> Pantalladebienvenida { get; set; }
        public virtual DbSet<Pedido> Pedido { get; set; }
        public virtual DbSet<Persona> Persona { get; set; }
        public virtual DbSet<Personadirecion> Personadirecion { get; set; }
        public virtual DbSet<Sucursal> Sucursal { get; set; }
        public virtual DbSet<Tipodedocumento> Tipodedocumento { get; set; }
        public virtual DbSet<Usuariodelivery> Usuariodelivery { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("server=deliverydatabasemysql.mysql.database.azure.com;database=deliveryhuancayo;user=skeys@deliverydatabasemysql;pwd=\"SUNAT besa mis pelotas 1\";charset=utf8;port=3306", x => x.ServerVersion("5.7.29-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Agrupacion>(entity =>
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

                entity.HasOne(d => d.CodigoPadreAgrupacionNavigation)
                    .WithMany(p => p.InverseCodigoPadreAgrupacionNavigation)
                    .HasForeignKey(d => d.CodigoPadreAgrupacion)
                    .HasConstraintName("fk_Agrupacion_Agrupacion1");
            });

            modelBuilder.Entity<Agrupacionempresa>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("agrupacionempresa");

                entity.HasIndex(e => e.AgrupacionCodigo)
                    .HasName("fk_AgrupacionEmpresa_Agrupacion1_idx");

                entity.HasIndex(e => e.EmpresaCodigo)
                    .HasName("fk_AgrupacionEmpresa_Empresa1_idx");

                entity.Property(e => e.AgrupacionCodigo)
                    .IsRequired()
                    .HasColumnName("Agrupacion_Codigo")
                    .HasColumnType("varchar(6)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.EmpresaCodigo)
                    .IsRequired()
                    .HasColumnName("Empresa_Codigo")
                    .HasColumnType("varchar(15)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.HasOne(d => d.AgrupacionCodigoNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.AgrupacionCodigo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_AgrupacionEmpresa_Agrupacion1");

                entity.HasOne(d => d.EmpresaCodigoNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.EmpresaCodigo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_AgrupacionEmpresa_Empresa1");
            });

            modelBuilder.Entity<Buscadorempresa>(entity =>
            {
                entity.ToTable("buscadorempresa");

                entity.HasIndex(e => e.EmpresaCodigo)
                    .HasName("fk_BuscadorEmpresa_Empresa1_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.EmpresaCodigo)
                    .IsRequired()
                    .HasColumnName("Empresa_Codigo")
                    .HasColumnType("varchar(15)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.PalabraClave)
                    .IsRequired()
                    .HasColumnType("varchar(15)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.HasOne(d => d.EmpresaCodigoNavigation)
                    .WithMany(p => p.Buscadorempresa)
                    .HasForeignKey(d => d.EmpresaCodigo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_BuscadorEmpresa_Empresa1");
            });

            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.HasKey(e => e.Codigo)
                    .HasName("PRIMARY");

                entity.ToTable("categoria");

                entity.HasIndex(e => e.CodigoPadreAgrupacion)
                    .HasName("fk_Categoria_Categoria1_idx");

                entity.HasIndex(e => e.EmpresaCodigo)
                    .HasName("fk_Categoria_Empresa1_idx");

                entity.Property(e => e.Codigo)
                    .HasColumnType("varchar(15)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Activo)
                    .IsRequired()
                    .HasColumnType("varchar(2)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.CantidadDeCategoria)
                    .IsRequired()
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.CodigoPadreAgrupacion)
                    .IsRequired()
                    .HasColumnType("varchar(15)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Descripcion)
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

                entity.Property(e => e.MenuPrincipal)
                    .IsRequired()
                    .HasColumnType("varchar(2)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.UrlImagen)
                    .HasColumnType("varchar(250)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.HasOne(d => d.CodigoPadreAgrupacionNavigation)
                    .WithMany(p => p.InverseCodigoPadreAgrupacionNavigation)
                    .HasForeignKey(d => d.CodigoPadreAgrupacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Categoria_Categoria1");

                entity.HasOne(d => d.EmpresaCodigoNavigation)
                    .WithMany(p => p.Categoria)
                    .HasForeignKey(d => d.EmpresaCodigo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Categoria_Empresa1");
            });

            modelBuilder.Entity<Categoriaitem>(entity =>
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
            });

            modelBuilder.Entity<Detallepedido>(entity =>
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
            });

            modelBuilder.Entity<Empresa>(entity =>
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
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.HasKey(e => e.Codigo)
                    .HasName("PRIMARY");

                entity.ToTable("item");

                entity.Property(e => e.Codigo)
                    .HasColumnType("varchar(25)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Activo)
                    .IsRequired()
                    .HasColumnType("varchar(2)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Descrpcion)
                    .HasColumnType("varchar(150)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.FechaDeUltimaModificacion).HasColumnType("datetime");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.TieneOferta)
                    .IsRequired()
                    .HasColumnType("varchar(2)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.UrlImagen)
                    .HasColumnType("varchar(250)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<Motorizado>(entity =>
            {
                entity.ToTable("motorizado");

                entity.HasIndex(e => e.EmpresaCodigo)
                    .HasName("fk_Motorizado_Empresa1_idx");

                entity.HasIndex(e => e.TipoDeDocumentoCodigo)
                    .HasName("fk_Motorizado_TipoDeDocumento1_idx");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.CodigoDeVerificacion)
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
                    .WithMany(p => p.Motorizado)
                    .HasForeignKey(d => d.EmpresaCodigo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Motorizado_Empresa1");

                entity.HasOne(d => d.TipoDeDocumentoCodigoNavigation)
                    .WithMany(p => p.Motorizado)
                    .HasForeignKey(d => d.TipoDeDocumentoCodigo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Motorizado_TipoDeDocumento1");
            });

            modelBuilder.Entity<Pantalladebienvenida>(entity =>
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
            });

            modelBuilder.Entity<Pedido>(entity =>
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

                entity.Property(e => e.FechaDeAnulacion).HasColumnType("datetime");

                entity.Property(e => e.FechaYhoraDeAprovacion)
                    .HasColumnName("FechaYHoraDeAprovacion")
                    .HasColumnType("datetime");

                entity.Property(e => e.FechaYhoraDeEntrega)
                    .HasColumnName("FechaYHoraDeEntrega")
                    .HasColumnType("datetime");

                entity.Property(e => e.FechaYhoraDeRegistro)
                    .HasColumnName("FechaYHoraDeRegistro")
                    .HasColumnType("datetime");

                entity.Property(e => e.FechaYhoraDeSalida)
                    .HasColumnName("FechaYHoraDeSalida")
                    .HasColumnType("datetime");

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
            });

            modelBuilder.Entity<Persona>(entity =>
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
            });

            modelBuilder.Entity<Personadirecion>(entity =>
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
            });

            modelBuilder.Entity<Sucursal>(entity =>
            {
                entity.ToTable("sucursal");

                entity.HasIndex(e => e.EmpresaCodigo)
                    .HasName("fk_Sucursal_Empresa1_idx");

                entity.Property(e => e.Id)
                    .HasColumnType("varchar(15)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Direccion)
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

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.HasOne(d => d.EmpresaCodigoNavigation)
                    .WithMany(p => p.Sucursal)
                    .HasForeignKey(d => d.EmpresaCodigo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Sucursal_Empresa1");
            });

            modelBuilder.Entity<Tipodedocumento>(entity =>
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
            });

            modelBuilder.Entity<Usuariodelivery>(entity =>
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
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
