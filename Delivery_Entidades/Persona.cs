using System;
using System.Collections.Generic;

namespace Delivery_Entidades
{
    public partial class Persona
    {
        public Persona()
        {
            Pedido = new HashSet<Pedido>();
        }

        public int Id { get; set; }
        public string TipoDeDocumentoCodigo { get; set; }
        public string NumeroDeDocumento { get; set; }
        public string Denominacion { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordReset { get; set; }
        public string CodigoDeVerificacion { get; set; }
        public string TelefonoVerificado { get; set; }

        public virtual Tipodedocumento TipoDeDocumentoCodigoNavigation { get; set; }
        public virtual ICollection<Pedido> Pedido { get; set; }
    }
}
