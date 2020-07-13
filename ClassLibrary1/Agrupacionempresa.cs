using System;
using System.Collections.Generic;

namespace ClassLibrary1
{
    public partial class Agrupacionempresa
    {
        public string AgrupacionCodigo { get; set; }
        public string EmpresaCodigo { get; set; }

        public virtual Agrupacion AgrupacionCodigoNavigation { get; set; }
        public virtual Empresa EmpresaCodigoNavigation { get; set; }
    }
}
