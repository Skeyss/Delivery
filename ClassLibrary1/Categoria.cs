using System;
using System.Collections.Generic;

namespace ClassLibrary1
{
    public partial class Categoria
    {
        public Categoria()
        {
            InverseCodigoPadreAgrupacionNavigation = new HashSet<Categoria>();
        }

        public string Codigo { get; set; }
        public string CodigoPadreAgrupacion { get; set; }
        public string EmpresaCodigo { get; set; }
        public string CantidadDeCategoria { get; set; }
        public string MenuPrincipal { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string UrlImagen { get; set; }
        public string Activo { get; set; }

        public virtual Categoria CodigoPadreAgrupacionNavigation { get; set; }
        public virtual Empresa EmpresaCodigoNavigation { get; set; }
        public virtual ICollection<Categoria> InverseCodigoPadreAgrupacionNavigation { get; set; }
    }
}
