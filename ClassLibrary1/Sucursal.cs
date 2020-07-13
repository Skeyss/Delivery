using System;
using System.Collections.Generic;

namespace ClassLibrary1
{
    public partial class Sucursal
    {
        public Sucursal()
        {
            Pedido = new HashSet<Pedido>();
        }

        public string Id { get; set; }
        public string EmpresaCodigo { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }

        public virtual Empresa EmpresaCodigoNavigation { get; set; }
        public virtual ICollection<Pedido> Pedido { get; set; }
    }
}
