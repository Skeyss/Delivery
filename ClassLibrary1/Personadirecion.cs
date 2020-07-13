using System;
using System.Collections.Generic;

namespace ClassLibrary1
{
    public partial class Personadirecion
    {
        public int PersonaId { get; set; }
        public string Direccion { get; set; }

        public virtual Persona Persona { get; set; }
    }
}
