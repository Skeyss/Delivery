using System;
using System.Collections.Generic;
using System.Text;

namespace Delivery.Models.Inicio
{
    public class Bienvenida
    {
        public string PathImagen { get; set; }
        public string MensajePrincipal { get; set; }
        public string MensajeSecundario { get; set; }

        public Bienvenida()
        {
            PathImagen = "";
            MensajePrincipal = "";
            MensajeSecundario = "";
        }
    }
}
