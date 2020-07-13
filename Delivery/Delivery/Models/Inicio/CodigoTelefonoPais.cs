using System;
using System.Collections.Generic;
using System.Text;

namespace Delivery.Models.Inicio
{
    public  class CodigoTelefonoPais
    {
        public string Codigo { get; set; }
        public string Nombre { get; set; }

        public CodigoTelefonoPais()
        {
            this.Codigo = "+51";
            this.Nombre = "Peru";
        }
    }
}
