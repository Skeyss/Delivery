using System;
using System.Collections.Generic;

namespace ClassLibrary1
{
    public partial class Buscadorempresa
    {
        public int Id { get; set; }
        public string EmpresaCodigo { get; set; }
        public string PalabraClave { get; set; }

        public virtual Empresa EmpresaCodigoNavigation { get; set; }
    }
}
