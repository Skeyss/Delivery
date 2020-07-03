using System;
using System.Collections.Generic;
using System.Text;

namespace Delivery_Dtos
{
    public class AgrupacionDto
    {
        public string Codigo { get; set; }
        public string CodigoPadreAgrupacion { get; set; }
        public int CantidadDeAgrupacionesHijo { get; set; }
        public bool MenuPrincipal { get; set; }
        public string Nombre { get; set; }
        public string UrlImagen { get; set; }
        public bool Activo { get; set; }
    }
}
