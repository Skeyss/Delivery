using System;
using System.Collections.Generic;
using System.Text;

namespace Delivery.Core.Mensaje
{
    public class Mensaje
    {
        public enum TipoDeMensaje
        {
            Error,
            Ok,
            Advertencia,
            Guia,
            Ninguno
        }

        public string MensajeGenerado { get; set; }
        //public Color ColorDeMensaje { get; set; }
      
        public string DetalleDelMensaje { get; set; }
  
        public Mensaje()
        {
            //this.ColorDeMensaje = System.Drawing.Color.Black;
            this.MensajeGenerado = "";
        }

        public void LimpiarMensaje()
        {
            this.MensajeGenerado = "";
            this.DetalleDelMensaje = "";
        }
      
    }
}
