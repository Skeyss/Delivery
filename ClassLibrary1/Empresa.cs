using System;
using System.Collections.Generic;

namespace ClassLibrary1
{
    public partial class Empresa
    {
        public Empresa()
        {
            Buscadorempresa = new HashSet<Buscadorempresa>();
            Categoria = new HashSet<Categoria>();
            Motorizado = new HashSet<Motorizado>();
            Pedido = new HashSet<Pedido>();
            Sucursal = new HashSet<Sucursal>();
            Usuariodelivery = new HashSet<Usuariodelivery>();
        }

        public string Codigo { get; set; }
        public string TipoDeDocumentoCodigo { get; set; }
        public string UrlImagen { get; set; }
        public string RazonSocialDenominacion { get; set; }
        public string NombreComercial { get; set; }
        public double PrecioDelivery { get; set; }
        public string TieneOfertaDelivery { get; set; }
        public double PrecioDeliveryOferta { get; set; }
        public double PorcentajePrecioDelivery { get; set; }
        public int MinutosMinDelivery { get; set; }
        public int MinutosMaxDelivery { get; set; }

        public virtual Tipodedocumento TipoDeDocumentoCodigoNavigation { get; set; }
        public virtual ICollection<Buscadorempresa> Buscadorempresa { get; set; }
        public virtual ICollection<Categoria> Categoria { get; set; }
        public virtual ICollection<Motorizado> Motorizado { get; set; }
        public virtual ICollection<Pedido> Pedido { get; set; }
        public virtual ICollection<Sucursal> Sucursal { get; set; }
        public virtual ICollection<Usuariodelivery> Usuariodelivery { get; set; }
    }
}
