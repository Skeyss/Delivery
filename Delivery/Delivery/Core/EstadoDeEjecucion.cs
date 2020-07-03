using System;
using System.Collections.Generic;
using System.Text;

namespace Delivery.Core
{
    public class EstadoDeEjecucion
    {
        public bool StatusProcesamiento { get; set; }
        public bool StatusInternet { get; set; }
        public Delivery.Core.Mensaje.Mensaje Mensaje { get; set; }

        public EstadoDeEjecucion()
        {

            this.StatusProcesamiento = false;
            this.StatusInternet = false;
            this.Mensaje = new Delivery.Core.Mensaje.Mensaje();
        }
    }
}
