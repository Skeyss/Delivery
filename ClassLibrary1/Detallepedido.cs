using System;
using System.Collections.Generic;

namespace ClassLibrary1
{
    public partial class Detallepedido
    {
        public int Id { get; set; }
        public int PedidoId { get; set; }
        public string ItemCodigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public double Cantidad { get; set; }
        public double PrecioUnitario { get; set; }
        public double MontoTotal { get; set; }
        public string Estado { get; set; }

        public virtual Item ItemCodigoNavigation { get; set; }
        public virtual Pedido Pedido { get; set; }
    }
}
