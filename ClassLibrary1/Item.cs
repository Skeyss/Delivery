using System;
using System.Collections.Generic;

namespace ClassLibrary1
{
    public partial class Item
    {
        public Item()
        {
            Detallepedido = new HashSet<Detallepedido>();
        }

        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descrpcion { get; set; }
        public string UrlImagen { get; set; }
        public double Precio { get; set; }
        public string TieneOferta { get; set; }
        public double PrecioOferta { get; set; }
        public double PorcentajeOferta { get; set; }
        public string Activo { get; set; }
        public DateTime FechaDeUltimaModificacion { get; set; }

        public virtual ICollection<Detallepedido> Detallepedido { get; set; }
    }
}
