using System;
using Delivery_Datos.Configuracion;
using Delivery_Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Delivery_Datos
{
    public partial class DeliveryContext : DbContext
    {
        public DeliveryContext()
        {
        }

        public DeliveryContext(DbContextOptions<DeliveryContext> options)
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

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseMySql("server=104.236.6.249;database=aaaaa;user=root;pwd=Intel-IT;charset=utf8;port=3306", x => x.ServerVersion("5.5.40-mysql"));
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AgrupacionConfiguration());

            modelBuilder.ApplyConfiguration(new AgrupacionempresaConfiguration());


            modelBuilder.ApplyConfiguration(new BuscadorempresaConfiguration());
            modelBuilder.ApplyConfiguration(new CategoriaConfiguration());

            modelBuilder.ApplyConfiguration(new CategoriaitemConfiguration());
            modelBuilder.ApplyConfiguration(new DetallepedidoConfiguration());
            modelBuilder.ApplyConfiguration(new EmpresaConfiguration());
            modelBuilder.ApplyConfiguration(new ItemConfiguration());
            modelBuilder.ApplyConfiguration(new MotorizadoConfiguration());
            modelBuilder.ApplyConfiguration(new PantalladebienvenidaConfiguration());
            
            modelBuilder.ApplyConfiguration(new PedidoConfiguration());
            modelBuilder.ApplyConfiguration(new PersonaConfiguration());

            modelBuilder.ApplyConfiguration(new PersonadirecionConfiguration());
            modelBuilder.ApplyConfiguration(new SucursalConfiguration());
            modelBuilder.ApplyConfiguration(new TipodedocumentoConfiguration());

            modelBuilder.ApplyConfiguration(new UsuariodeliveryConfiguration());

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
