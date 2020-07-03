using System;
using System.Collections.Generic;
using System.Text;

namespace Delivery.Models.Inicio
{
    public class Menu
    {
        public enum TipoDeMenu
        {
            Navegacion,
            Informacion
        }

        public TipoDeMenu tipoDeMenu { get; set; }
        public string titulo { get; set; }


        public Menu()
        {
            this.tipoDeMenu = TipoDeMenu.Navegacion;
            this.titulo = "";
        }

    
    }
}
