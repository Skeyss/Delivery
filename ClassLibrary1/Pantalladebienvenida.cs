using System;
using System.Collections.Generic;

namespace ClassLibrary1
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
