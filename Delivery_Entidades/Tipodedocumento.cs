using System;
using System.Collections.Generic;

namespace Delivery_Entidades
{
    public partial class Tipodedocumento
    {
        public Tipodedocumento()
        {
            Empresa = new HashSet<Empresa>();
            Motorizado = new HashSet<Motorizado>();
            Persona = new HashSet<Persona>();
            Usuariodelivery = new HashSet<Usuariodelivery>();
        }

        public string Codigo { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Empresa> Empresa { get; set; }
        public virtual ICollection<Motorizado> Motorizado { get; set; }
        public virtual ICollection<Persona> Persona { get; set; }
        public virtual ICollection<Usuariodelivery> Usuariodelivery { get; set; }
    }
}
