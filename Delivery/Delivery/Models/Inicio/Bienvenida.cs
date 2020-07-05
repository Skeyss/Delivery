using System;
using System.Collections.Generic;
using System.Text;

namespace Delivery.Models.Inicio
{
    public class Bienvenida
    {
        public int Id { get; set; }
        public string UrlImagen { get; set; }
        public string MensajePrincipal { get; set; }
        public string MensajeSecundario { get; set; }
        public int? OrdenDeVisualizacion { get; set; }

        public Bienvenida()
        {
            this.Id = 0;
            this.UrlImagen = "";
            this.MensajePrincipal = "";
            this.MensajeSecundario = "";
            this.OrdenDeVisualizacion = 0;
        }
    }
}
