using System;
using System.Collections.Generic;
using System.Text;

namespace Delivery.Core
{
    public class EstadoDeConsulta
    {
        public bool StatusProcesamiento { get; set; }
        public bool StatusInternet { get; set; }
        public object ValorObjeto { get; set; }
        public Delivery.Core.Mensaje.Mensaje Mensaje { get; set; }

        public EstadoDeConsulta()
        {
            this.StatusProcesamiento = false;
            this.StatusInternet = false;
            this.ValorObjeto = null; 
            this.Mensaje = new Delivery.Core.Mensaje.Mensaje();
        }
    }
}
