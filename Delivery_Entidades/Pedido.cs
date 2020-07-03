using Delivery_Entidades.Enum;
using System;
using System.Collections.Generic;

namespace Delivery_Entidades
{
    public partial class Pedido
    {
        public Pedido()
        {
            Detallepedido = new HashSet<Detallepedido>();
        }

        public int Id { get; set; }
        public string EmpresaCodigo { get; set; }
        public string SucursalId { get; set; }
        public int PersonaId { get; set; }
        public string NumeroDeDocumento { get; set; }
        public string Denominacion { get; set; }
        public string Telefono { get; set; }
        public double MontoTotal { get; set; }
        public EstadoPedido Estado { get; set; }
        public DateTime FechaYhoraDeRegistro { get; set; }
        public string FechaYhoraProgramadaDeEnvio { get; set; }
        public DateTime? FechaDeAnulacion { get; set; }
        public DateTime? FechaYhoraDeAprovacion { get; set; }
        public DateTime? FechaYhoraDeSalida { get; set; }
        public DateTime? FechaYhoraDeEntrega { get; set; }
        public string Tipo { get; set; }
        public int? IdUsuarioAprobacion { get; set; }
        public int? IdMotorizadoAprobacion { get; set; }
        public int? IdUsuarioSalida { get; set; }
        public int? IdMotorizadoSalida { get; set; }
        public int? IdUusarioEntrega { get; set; }
        public int? IdMotorizadoEntrega { get; set; }

        public virtual Empresa EmpresaCodigoNavigation { get; set; }
        public virtual Persona Persona { get; set; }
        public virtual Sucursal Sucursal { get; set; }
        public virtual ICollection<Detallepedido> Detallepedido { get; set; }
    }
}
