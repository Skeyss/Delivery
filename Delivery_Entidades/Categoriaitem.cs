using System;
using System.Collections.Generic;

namespace Delivery_Entidades
{
    public partial class Categoriaitem
    {
        public string CategoriaCodigo { get; set; }
        public string ItemCodigo { get; set; }

        public virtual Categoria CategoriaCodigoNavigation { get; set; }
        public virtual Item ItemCodigoNavigation { get; set; }
    }
}
