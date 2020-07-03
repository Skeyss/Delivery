using System;
using System.Collections.Generic;

namespace Delivery_Entidades
{
    public partial class Agrupacionempresa
    {
        public string AgrupacionCodigo { get; set; }
        public string EmpresaCodigo { get; set; }

        public virtual Agrupacion AgrupacionCodigoNavigation { get; set; }
        public virtual Empresa EmpresaCodigoNavigation { get; set; }
    }
}
