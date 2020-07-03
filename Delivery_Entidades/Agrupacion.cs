using System;
using System.Collections.Generic;

namespace Delivery_Entidades
{
    public partial class Agrupacion
    {
        public Agrupacion()
        {
            //InverseCodigoPadreAgrupacionNavigation = new HashSet<Agrupacion>();
        }

        public string Codigo { get; set; }
        public string CodigoPadreAgrupacion { get; set; }
        public int CantidadDeAgrupacionesHijo { get; set; }
        public string MenuPrincipal { get; set; }
        public string Nombre { get; set; }
        public string UrlImagen { get; set; }
        public string Activo { get; set; }

       // public virtual Agrupacion CodigoPadreAgrupacionNavigation { get; set; }
       // public virtual ICollection<Agrupacion> InverseCodigoPadreAgrupacionNavigation { get; set; }
    }
}
