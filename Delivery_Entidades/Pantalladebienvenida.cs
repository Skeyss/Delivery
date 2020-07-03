using System;
using System.Collections.Generic;

namespace Delivery_Entidades
{
    public partial class Pantalladebienvenida
    {
        public int Id { get; set; }
        public string UrlImagen { get; set; }
        public string MensajePrincipal { get; set; }
        public string MensajeSecundario { get; set; }
        public int? OrdenDeVisualizacion { get; set; }
    }
}
