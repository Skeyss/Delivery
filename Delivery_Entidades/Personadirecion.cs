using System;
using System.Collections.Generic;

namespace Delivery_Entidades
{
    public partial class Personadirecion
    {
        public int PersonaId { get; set; }
        public string Direccion { get; set; }

        public virtual Persona Persona { get; set; }
    }
}
